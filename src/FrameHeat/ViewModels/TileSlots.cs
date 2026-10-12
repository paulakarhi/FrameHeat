using System.Collections.ObjectModel;

namespace FrameHeat.ViewModels;

/// <summary>
/// The "show in the widget" list in the order the tiles are shown. When the order changes the rows
/// are moved, not rebuilt, so a row keeps its keyboard focus while the user clicks its arrow.
/// </summary>
internal sealed class TileSlots : IDisposable
{
    private readonly TileVisibility _tiles;

    public TileSlots(TileVisibility tiles)
    {
        _tiles = tiles;
        foreach (var kind in tiles.Order)
            Items.Add(new TileSlot(kind, tiles));

        tiles.PropertyChanged += OnTilesChanged;
    }

    public ObservableCollection<TileSlot> Items { get; } = [];

    public void Dispose() => _tiles.PropertyChanged -= OnTilesChanged;

    private void OnTilesChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        foreach (var slot in Items)
            slot.VisibilityChangedElsewhere();

        if (e.PropertyName != nameof(TileVisibility.Order))
            return;

        for (var position = 0; position < _tiles.Order.Length; position++)
        {
            var current = Items.IndexOf(Items.First(slot => slot.Kind == _tiles.Order[position]));
            if (current != position)
                Items.Move(current, position);
        }
    }
}
