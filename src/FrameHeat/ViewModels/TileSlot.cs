using System.Windows.Input;

namespace FrameHeat.ViewModels;

/// <summary>One entry of the "show in the widget" list: a kind of tile with its on/off switch and arrows to move it.</summary>
internal sealed class TileSlot : VisibilitySlot
{
    public TileSlot(MetricKind kind, TileVisibility tiles)
        : base(() => tiles.IsVisible(kind), visible => tiles.SetVisible(kind, visible))
    {
        Kind = kind;
        MoveUpCommand = new RelayCommand(() => tiles.Move(kind, -1));
        MoveDownCommand = new RelayCommand(() => tiles.Move(kind, 1));
    }

    public MetricKind Kind { get; }

    public string Label => Kind switch
    {
        MetricKind.Cpu => Loc.T("show_cpu"),
        MetricKind.Gpu => Loc.T("show_gpu"),
        MetricKind.Vram => Loc.T("show_vram"),
        MetricKind.Memory => Loc.T("show_ram"),
        MetricKind.Disk => Loc.T("show_disks"),
        MetricKind.Fps => Loc.T("show_fps"),
        MetricKind.Network => Loc.T("show_net"),
        _ => Loc.T("show_ping"),
    };

    public ICommand MoveUpCommand { get; }
    public ICommand MoveDownCommand { get; }
}
