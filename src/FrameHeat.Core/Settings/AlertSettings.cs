using System.ComponentModel;
using FrameHeat.Core.Alerts;
using FrameHeat.Core.Metrics;

namespace FrameHeat.Core.Settings;

/// <summary>The temperature limits for each kind of device, and whether a device's own limit wins over them.</summary>
public sealed class AlertSettings : ObservableObject
{
    private bool _useDeviceLimits = true;
    private AlertLimits _cpu = DefaultLimits.ForTemperature(MetricKind.Cpu);
    private AlertLimits _gpu = DefaultLimits.ForTemperature(MetricKind.Gpu);
    private AlertLimits _vram = DefaultLimits.ForTemperature(MetricKind.Vram);
    private AlertLimits _memory = DefaultLimits.ForTemperature(MetricKind.Memory);
    private AlertLimits _disk = DefaultLimits.ForTemperature(MetricKind.Disk);

    // A change in any device's limits is reported as a change of that property on this object.
    public AlertSettings()
    {
        _cpu.PropertyChanged += OnLimitsChanged;
        _gpu.PropertyChanged += OnLimitsChanged;
        _vram.PropertyChanged += OnLimitsChanged;
        _memory.PropertyChanged += OnLimitsChanged;
        _disk.PropertyChanged += OnLimitsChanged;
    }

    /// <summary>Use the limits disks and memory report for themselves; the configured limits are the fallback.</summary>
    public bool UseDeviceLimits { get => _useDeviceLimits; set => Set(ref _useDeviceLimits, value); }

    public AlertLimits Cpu { get => _cpu; set => SetChild(ref _cpu, value, OnLimitsChanged); }
    public AlertLimits Gpu { get => _gpu; set => SetChild(ref _gpu, value, OnLimitsChanged); }
    public AlertLimits Vram { get => _vram; set => SetChild(ref _vram, value, OnLimitsChanged); }
    public AlertLimits Memory { get => _memory; set => SetChild(ref _memory, value, OnLimitsChanged); }
    public AlertLimits Disk { get => _disk; set => SetChild(ref _disk, value, OnLimitsChanged); }

    /// <summary>The limits of a kind of device. A frame rate has no temperature limits.</summary>
    public AlertLimits For(MetricKind kind) => kind switch
    {
        MetricKind.Cpu => Cpu,
        MetricKind.Gpu => Gpu,
        MetricKind.Vram => Vram,
        MetricKind.Memory => Memory,
        MetricKind.Disk => Disk,
        _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, "This kind has no temperature limits."),
    };

    public void ResetToDefaults()
    {
        foreach (var kind in MetricKinds.WithTemperature)
        {
            var defaults = DefaultLimits.ForTemperature(kind);
            var limits = For(kind);

            // Warning first: raising it may push the critical limit up, which is then overwritten.
            limits.Warning = defaults.Warning;
            limits.Critical = defaults.Critical;
        }
    }

    private void OnLimitsChanged(object? sender, PropertyChangedEventArgs e)
    {
        var name = ReferenceEquals(sender, _cpu) ? nameof(Cpu)
                 : ReferenceEquals(sender, _gpu) ? nameof(Gpu)
                 : ReferenceEquals(sender, _vram) ? nameof(Vram)
                 : ReferenceEquals(sender, _memory) ? nameof(Memory)
                 : nameof(Disk);
        OnPropertyChanged(name);
    }
}
