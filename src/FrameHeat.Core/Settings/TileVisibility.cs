using FrameHeat.Core.Metrics;

namespace FrameHeat.Core.Settings;

/// <summary>
/// Which tiles are shown and in what order. All disks are switched on or off together, and each
/// disk can also be hidden on its own.
/// </summary>
public sealed class TileVisibility : ObservableObject
{
    private static readonly MetricKind[] DefaultOrder = Enum.GetValues<MetricKind>();

    private bool _cpu = true;
    private bool _gpu = true;
    private bool _vram = true;
    private bool _memory = true;
    private bool _disks = true;
    private bool _fps = true;
    private MetricKind[] _order = [.. DefaultOrder];
    private string[] _hiddenDisks = [];

    public bool Cpu { get => _cpu; set => Set(ref _cpu, value); }
    public bool Gpu { get => _gpu; set => Set(ref _gpu, value); }
    public bool Vram { get => _vram; set => Set(ref _vram, value); }
    public bool Memory { get => _memory; set => Set(ref _memory, value); }
    public bool Disks { get => _disks; set => Set(ref _disks, value); }
    public bool Fps { get => _fps; set => Set(ref _fps, value); }

    /// <summary>The kinds of tile from first to last. Every kind appears exactly once; a saved order is repaired on load.</summary>
    public MetricKind[] Order { get => _order; set => Set(ref _order, Normalize(value)); }

    /// <summary>Keys of the disks the user has switched off.</summary>
    public string[] HiddenDisks { get => _hiddenDisks; set => Set(ref _hiddenDisks, [.. (value ?? []).Distinct()]); }

    public bool IsVisible(MetricKind kind) => kind switch
    {
        MetricKind.Cpu => Cpu,
        MetricKind.Gpu => Gpu,
        MetricKind.Vram => Vram,
        MetricKind.Memory => Memory,
        MetricKind.Disk => Disks,
        _ => Fps,
    };

    public bool IsVisible(MetricReading reading)
        => IsVisible(reading.Kind) && !(reading.Kind == MetricKind.Disk && _hiddenDisks.Contains(reading.Key));

    public void SetVisible(MetricKind kind, bool visible)
    {
        switch (kind)
        {
            case MetricKind.Cpu: Cpu = visible; break;
            case MetricKind.Gpu: Gpu = visible; break;
            case MetricKind.Vram: Vram = visible; break;
            case MetricKind.Memory: Memory = visible; break;
            case MetricKind.Disk: Disks = visible; break;
            default: Fps = visible; break;
        }
    }

    public bool IsDiskVisible(string diskKey) => !_hiddenDisks.Contains(diskKey);

    public void SetDiskVisible(string diskKey, bool visible)
        => HiddenDisks = visible ? [.. _hiddenDisks.Where(key => key != diskKey)] : [.. _hiddenDisks, diskKey];

    public int PositionOf(MetricKind kind) => Array.IndexOf(_order, kind);

    /// <summary>Moves a tile kind up (negative) or down (positive) in the order, stopping at the ends.</summary>
    public void Move(MetricKind kind, int steps)
    {
        var from = PositionOf(kind);
        var to = Math.Clamp(from + steps, 0, _order.Length - 1);
        if (from < 0 || to == from)
            return;

        var moved = _order.ToList();
        moved.RemoveAt(from);
        moved.Insert(to, kind);
        Order = [.. moved];
    }

    // Keeps the first occurrence of each kind and appends the kinds that are missing.
    private static MetricKind[] Normalize(MetricKind[]? order)
        => [.. (order ?? []).Where(Enum.IsDefined).Distinct().Concat(DefaultOrder).Distinct()];
}
