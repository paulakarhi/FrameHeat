using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using FrameHeat.Layout;
using FrameHeat.Native;
using FrameHeat.Services;
using FrameHeat.ViewModels;
using DrawingRectangle = System.Drawing.Rectangle;

namespace FrameHeat.Views;

/// <summary>
/// The widget window: transparent, always on top, never activated. Clicks pass through to the
/// game underneath except while the widget is being edited.
/// </summary>
internal sealed partial class OverlayWindow : Window
{
    // Gap between the widget and the screen edge when it is snapped to a corner.
    private const double ScreenMargin = 24;
    private static readonly TimeSpan TopmostInterval = TimeSpan.FromSeconds(2);

    private readonly OverlayViewModel _viewModel;
    private readonly PlacementTracker _placementTracker;
    private readonly DispatcherTimer _topmostTimer;
    private IntPtr _handle;

    // False while the window still shows the "waiting for sensors" placeholder, so the first
    // growth into real tiles is not treated as an ordinary resize (see OnSizeChanged).
    private bool _sizedForContent;

    // The failed restore is logged once, not on every retry.
    private bool _reportedPlacementMiss;

    public OverlayWindow(OverlayViewModel viewModel, WindowPlacement placement)
    {
        _viewModel = viewModel;
        _placementTracker = new PlacementTracker(placement);

        InitializeComponent();
        DataContext = viewModel;

        // The window handle exists only after SourceInitialized; placement needs the final size, so it waits for Loaded.
        SourceInitialized += OnSourceInitialized;
        Loaded += (_, _) => PlaceInitially();
        MouseLeftButtonDown += OnMouseLeftButtonDown;
        LocationChanged += OnLocationChanged;
        SizeChanged += OnSizeChanged;
        viewModel.PropertyChanged += OnViewModelPropertyChanged;

        // Full-screen games can raise themselves above the widget; reassert the order periodically.
        _topmostTimer = new DispatcherTimer(DispatcherPriority.Background, Dispatcher) { Interval = TopmostInterval };
        _topmostTimer.Tick += (_, _) =>
        {
            KeepTopmost();
            TryRestorePlacement();
        };
    }

    /// <summary>Moves the widget to a corner of the monitor it is on and remembers that as its position.</summary>
    public void SnapTo(ScreenCorner corner)
    {
        _placementTracker.MarkMovedByUser();
        UpdateLayout();
        var area = ToDips(WindowTools.MonitorWorkingArea(_handle));
        MoveTo(ScreenGeometry.CornerPosition(area, new Size(ActualWidth, ActualHeight), corner, ScreenMargin));

        // Saved even when the widget already sat in that corner, so the choice replaces an older position.
        SavePlacement();
    }

    // The native window exists now: set its click-through style, listen for drags and start the topmost timer.
    private void OnSourceInitialized(object? sender, EventArgs e)
    {
        _handle = new WindowInteropHelper(this).Handle;
        ApplyInteractionStyle();
        HwndSource.FromHwnd(_handle)?.AddHook(WndProc);
        _topmostTimer.Start();
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(OverlayViewModel.EditMode) && _handle != IntPtr.Zero)
            ApplyInteractionStyle();
    }

    // Outside edit mode the window ignores the mouse entirely so clicks reach the game; edit mode makes it draggable.
    private void ApplyInteractionStyle()
        => WindowTools.ApplyOverlayStyles(_handle, clickThrough: !_viewModel.EditMode);

    // WM_MOVING arrives while the window is being dragged; it is kept on the monitor under the cursor.
    private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        if (msg != User32.WmMoving)
            return IntPtr.Zero;

        // Only the user dragging the window sends this; the saved position is theirs to replace.
        _placementTracker.MarkMovedByUser();
        WindowTools.ConstrainDrag(lParam);
        handled = true;
        return new IntPtr(1);
    }

    private void KeepTopmost()
    {
        if (_handle != IntPtr.Zero && IsVisible && _viewModel.AlwaysOnTop)
            WindowTools.KeepTopmost(_handle);
    }

    // First placement: the saved position if the screens allow it, otherwise the top-left corner.
    private void PlaceInitially()
    {
        // The window may already hold its tiles, or still show the placeholder text.
        _sizedForContent = _viewModel.HasMetrics;

        if (TryRestorePlacement())
            return;

        var workArea = ToDips(WindowTools.MonitorWorkingArea(_handle));
        var corner = ScreenGeometry.CornerPosition(workArea, new Size(ActualWidth, ActualHeight), ScreenCorner.TopLeft, ScreenMargin);
        Left = corner.X;
        Top = corner.Y;
    }

    // Applies the saved position once it lies on a screen. Right after logon the screens may not be
    // ready yet, so this is retried from the timer instead of giving the position up.
    private bool TryRestorePlacement()
    {
        if (!IsLoaded || !_placementTracker.IsPending)
            return false;

        var screens = ToDips(WindowTools.VirtualScreenBounds());
        if (_placementTracker.TryRestore(screens) is { } position)
        {
            var inside = ScreenGeometry.ClampTopLeft(screens, position, new Size(ActualWidth, ActualHeight));
            Left = inside.X;
            Top = inside.Y;
            if (_reportedPlacementMiss)
                Log.Info("The saved widget position was restored");

            return true;
        }

        if (!_reportedPlacementMiss)
        {
            Log.Info($"The saved widget position ({_placementTracker.Saved?.X:0}, {_placementTracker.Saved?.Y:0}) is outside the screens {screens}; it is kept and tried again");
            _reportedPlacementMiss = true;
        }

        return false;
    }

    // A size change (layout, scale, tile count) must not push the widget off its edge or leave it
    // floating away from it, so the window keeps the screen edges it was closest to.
    private void OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (!IsLoaded || e.PreviousSize.Width < 1 || e.PreviousSize.Height < 1)
            return;

        var firstContent = !_sizedForContent && _viewModel.HasMetrics;
        _sizedForContent |= firstContent;

        var bounds = ToDips(WindowTools.MonitorBounds(_handle));
        MoveTo(ScreenGeometry.AfterResize(bounds, new Point(Left, Top), e.PreviousSize, e.NewSize, firstContent));
    }

    // In edit mode the whole widget is a drag handle.
    private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (!_viewModel.EditMode || e.ButtonState != MouseButtonState.Pressed)
            return;

        try
        {
            DragMove();
        }
        catch (InvalidOperationException)
        {
            // The button was released before the drag started.
        }
    }

    // Dragging is the only thing that moves the window in edit mode, so every change is a position to remember.
    private void OnLocationChanged(object? sender, EventArgs e)
    {
        if (_viewModel.EditMode && IsLoaded)
            SavePlacement();
    }

    // Sets the window position, ignoring sub-pixel differences, and remembers it when it actually changed.
    private void MoveTo(Point position)
    {
        var moved = false;

        if (Math.Abs(position.X - Left) > 0.5)
        {
            Left = position.X;
            moved = true;
        }

        if (Math.Abs(position.Y - Top) > 0.5)
        {
            Top = position.Y;
            moved = true;
        }

        if (moved)
            SavePlacement();
    }

    private void SavePlacement() => _placementTracker.Save(new Point(Left, Top));

    // Screen APIs report pixels but WPF positions windows in device-independent units; they differ on scaled displays.
    private Rect ToDips(DrawingRectangle pixels)
    {
        var dpi = VisualTreeHelper.GetDpi(this);
        return new Rect(
            pixels.X / dpi.DpiScaleX,
            pixels.Y / dpi.DpiScaleY,
            pixels.Width / dpi.DpiScaleX,
            pixels.Height / dpi.DpiScaleY);
    }
}
