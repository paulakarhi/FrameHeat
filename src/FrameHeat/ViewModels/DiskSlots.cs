using System.Collections.ObjectModel;

namespace FrameHeat.ViewModels;

/// <summary>The disks found in the machine, each with a switch for showing it in the widget.</summary>
internal sealed class DiskSlots : IDisposable
{
    private readonly TileVisibility _tiles;

    public DiskSlots(TileVisibility tiles)
    {
        _tiles = tiles;
        tiles.PropertyChanged += OnTilesChanged;
    }

    public ObservableCollection<DiskSlot> Items { get; } = [];

    public void Dispose() => _tiles.PropertyChanged -= OnTilesChanged;

    /// <summary>Brings the list up to date with the disks of a snapshot: new ones are added, ones that went away are removed.</summary>
    public void Refresh(SensorSnapshot? snapshot)
    {
        var disks = snapshot?.Metrics.Where(metric => metric.Kind == MetricKind.Disk).ToList() ?? [];

        foreach (var gone in Items.Where(slot => disks.All(disk => disk.Key != slot.Key)).ToList())
            Items.Remove(gone);

        foreach (var disk in disks.Where(disk => Items.All(slot => slot.Key != disk.Key)))
            Items.Add(new DiskSlot(disk.Key, $"{MetricLabels.For(disk)}: {disk.DeviceName}", _tiles));
    }

    // The list of hidden disks may be changed somewhere else, for example by loading the settings.
    private void OnTilesChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(TileVisibility.HiddenDisks))
            return;

        foreach (var slot in Items)
            slot.VisibilityChangedElsewhere();
    }
}

/// <summary>One disk in the list of disks to show.</summary>
internal sealed class DiskSlot(string key, string label, TileVisibility tiles)
    : VisibilitySlot(() => tiles.IsDiskVisible(key), visible => tiles.SetDiskVisible(key, visible))
{
    public string Key { get; } = key;

    public string Label { get; } = label;
}
