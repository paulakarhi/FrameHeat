using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using FrameHeat.ViewModels;

namespace FrameHeat.Views;

/// <summary>One device's tile. All content comes from the bound <see cref="MetricViewModel"/>.</summary>
internal sealed partial class MetricTile : UserControl
{
    // A new tile is laid out once before its bindings have been applied, which gives a size it will never
    // have again. The size is only counted from the moment that first layout is done.
    private bool _settled;

    public MetricTile()
    {
        InitializeComponent();
        DataContextChanged += OnDataContextChanged;
        Unloaded += (_, _) => Watch(DataContext as INotifyPropertyChanged, watching: false);
        Dispatcher.BeginInvoke(Settle, DispatcherPriority.Loaded);
    }

    // The digits of a reading come and go, and a tile that followed every one of them would make the whole
    // widget jump. So the tile keeps the room it has needed so far as its minimum size, until its look changes.
    // The size is read after the layout pass, when the content has its final size for the moment.
    protected override Size ArrangeOverride(Size finalSize)
    {
        var arranged = base.ArrangeOverride(finalSize);
        if (_settled && Content is FrameworkElement content)
        {
            MinWidth = Math.Max(MinWidth, content.DesiredSize.Width);
            MinHeight = Math.Max(MinHeight, content.DesiredSize.Height);
        }

        return arranged;
    }

    private void Settle()
    {
        _settled = true;
        InvalidateArrange();
    }

    private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        Watch(e.OldValue as INotifyPropertyChanged, watching: false);
        Watch(e.NewValue as INotifyPropertyChanged, watching: true);
    }

    private void Watch(INotifyPropertyChanged? tile, bool watching)
    {
        if (tile is null)
            return;

        if (watching)
            tile.PropertyChanged += OnTileChanged;
        else
            tile.PropertyChanged -= OnTileChanged;
    }

    // A change of design, text size, labels or usage is a new layout, not just a number changing.
    private void OnTileChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is not (nameof(MetricViewModel.Design) or nameof(MetricViewModel.TextScale)
            or nameof(MetricViewModel.ShowLabel) or nameof(MetricViewModel.ShowUsage)))
            return;

        ClearValue(MinWidthProperty);
        ClearValue(MinHeightProperty);
    }
}
