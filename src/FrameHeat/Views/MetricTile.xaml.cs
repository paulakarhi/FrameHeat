using System.Windows.Controls;

namespace FrameHeat.Views;

/// <summary>One device's tile. All content comes from the bound <see cref="ViewModels.MetricViewModel"/>.</summary>
internal sealed partial class MetricTile : UserControl
{
    public MetricTile() => InitializeComponent();
}
