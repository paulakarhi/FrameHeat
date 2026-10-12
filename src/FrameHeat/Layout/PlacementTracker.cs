using System.Windows;

namespace FrameHeat.Layout;

/// <summary>
/// Holds the widget's saved position. A saved position that cannot be applied yet, for example
/// because the screens were not ready right after logon, is kept as it is and applied later. It is
/// replaced only once the user has moved the widget.
/// </summary>
internal sealed class PlacementTracker(WindowPlacement stored)
{
    public bool IsPending { get; private set; } = stored is { Left: not null, Top: not null };

    public Point? Saved => stored is { Left: { } left, Top: { } top } ? new Point(left, top) : null;

    /// <summary>The saved position once it lies on the given screens; null while it does not, or when none is saved.</summary>
    public Point? TryRestore(Rect screens)
    {
        if (!IsPending || Saved is not { } position || !ScreenGeometry.IsReachable(screens, position))
            return null;

        IsPending = false;
        return position;
    }

    public void MarkMovedByUser() => IsPending = false;

    public void Save(Point position)
    {
        if (IsPending)
            return;

        stored.Left = position.X;
        stored.Top = position.Y;
    }
}
