using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FrameHeat.ViewModels;

/// <summary>The widget as a whole: its tiles plus the panel-level appearance derived from the settings.</summary>
internal sealed class OverlayViewModel : ObservableObject
{
    private readonly AppSettings _settings;

    // Kept so the tiles can be recomputed when a setting changes between two snapshots.
    private SensorSnapshot? _lastSnapshot;

    private Orientation _orientation = Orientation.Horizontal;
    private double _scale = 1.0;
    private double _textScale = 1.0;
    private double _widgetOpacity = 1.0;
    private TileShape _shape = TileShape.From(new AppearanceSettings());
    private bool _editMode;
    private bool _alwaysOnTop = true;
    private bool _hasMetrics;
    private Brush _panelBackground = Brushes.Transparent;
    private Brush _panelOutline = Brushes.Transparent;
    private Brush _accentBrush = Palette.Accent(AppearanceSettings.DefaultAccentColor);

    public OverlayViewModel(AppSettings settings)
    {
        _settings = settings;
        settings.Changed += OnSettingsChanged;

        ToggleOrientationCommand = new RelayCommand(() => _settings.Appearance.ToggleOrientation());
        OpenSettingsCommand = new RelayCommand(() => SettingsRequested?.Invoke());
        EndEditCommand = new RelayCommand(() => EditMode = false);

        ApplyAppearance();
    }

    /// <summary>Raised when the user asks for the settings window from the edit toolbar.</summary>
    public event Action? SettingsRequested;

    public ObservableCollection<MetricViewModel> Metrics { get; } = [];

    /// <summary>The readings the tiles were last built from; null before the first one.</summary>
    public SensorSnapshot? LastSnapshot => _lastSnapshot;

    public ICommand ToggleOrientationCommand { get; }
    public ICommand OpenSettingsCommand { get; }
    public ICommand EndEditCommand { get; }

    public Orientation Orientation { get => _orientation; private set => Set(ref _orientation, value); }
    public double Scale { get => _scale; private set => Set(ref _scale, value); }
    public double TextScale { get => _textScale; private set => Set(ref _textScale, value); }
    public double WidgetOpacity { get => _widgetOpacity; private set => Set(ref _widgetOpacity, value); }
    public TileShape Shape { get => _shape; private set => Set(ref _shape, value); }

    public bool HasMetrics { get => _hasMetrics; private set => Set(ref _hasMetrics, value); }
    public Brush PanelBackground { get => _panelBackground; private set => Set(ref _panelBackground, value); }
    public Brush PanelOutline { get => _panelOutline; private set => Set(ref _panelOutline, value); }
    public Brush AccentBrush { get => _accentBrush; private set => Set(ref _accentBrush, value); }

    /// <summary>While editing, the widget accepts the mouse so it can be dragged.</summary>
    public bool EditMode { get => _editMode; set => Set(ref _editMode, value); }

    /// <summary>Whether the widget stays above games and other windows.</summary>
    public bool AlwaysOnTop { get => _alwaysOnTop; private set => Set(ref _alwaysOnTop, value); }

    /// <summary>Applies a new snapshot. Must be called on the UI thread.</summary>
    public void Update(SensorSnapshot snapshot)
    {
        _lastSnapshot = snapshot;

        // Reuse the tile of a device that is still present and create tiles for new ones. Devices the
        // user has hidden get none, and the rest follow the order chosen in the settings.
        var existing = Metrics.ToDictionary(metric => metric.Key);
        var desired = new List<MetricViewModel>();
        var shown = snapshot.Metrics
            .Where(_settings.Tiles.IsVisible)
            .OrderBy(reading => _settings.Tiles.PositionOf(reading.Kind));

        foreach (var reading in shown)
        {
            var metric = existing.TryGetValue(reading.Key, out var found) ? found : new MetricViewModel(reading.Key);
            metric.Apply(reading, _settings);
            desired.Add(metric);
        }

        // Touch the collection only when the set of tiles changes, so running animations are not restarted.
        if (!Metrics.SequenceEqual(desired))
        {
            Metrics.Clear();
            foreach (var metric in desired)
                Metrics.Add(metric);
        }

        HasMetrics = Metrics.Count > 0;
    }

    // A changed setting redraws the panel and every tile from the last snapshot, without waiting for the next reading.
    private void OnSettingsChanged(SettingsChange change)
    {
        // Moving the window must not recompute every tile.
        if (change.Area is SettingsArea.Placement)
            return;

        // The other general settings (start with Windows) do not change what the widget shows.
        if (change.Area is SettingsArea.General)
        {
            AlwaysOnTop = _settings.AlwaysOnTop;
            return;
        }

        ApplyAppearance();
        if (_lastSnapshot is not null)
            Update(_lastSnapshot);
    }

    // Copies the panel-level settings (layout, size, shape, background, accent) into the properties the window binds to.
    private void ApplyAppearance()
    {
        var appearance = _settings.Appearance;

        AlwaysOnTop = _settings.AlwaysOnTop;
        Orientation = appearance.Orientation == PanelOrientation.Vertical ? Orientation.Vertical : Orientation.Horizontal;
        Scale = appearance.Scale;
        TextScale = appearance.TextScale;
        WidgetOpacity = appearance.WidgetOpacity;
        Shape = TileShape.From(appearance);
        PanelBackground = Palette.PanelBackground(appearance.PanelStyle, appearance.BackgroundOpacity);
        PanelOutline = Palette.PanelOutline(appearance.PanelStyle);
        AccentBrush = Palette.Accent(appearance.AccentColor);
    }
}
