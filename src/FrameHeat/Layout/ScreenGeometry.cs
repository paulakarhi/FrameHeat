using System.Windows;
using DrawingRectangle = System.Drawing.Rectangle;

namespace FrameHeat.Layout;

/// <summary>Pure placement math for the widget window; no window or screen access.</summary>
internal static class ScreenGeometry
{
    /// <summary>Moves a rectangle, in pixels, the shortest way into <paramref name="bounds"/> without resizing it.</summary>
    public static DrawingRectangle ClampInside(DrawingRectangle window, DrawingRectangle bounds)
    {
        var area = new Rect(bounds.X, bounds.Y, bounds.Width, bounds.Height);
        var corner = ClampTopLeft(area, new Point(window.X, window.Y), new Size(window.Width, window.Height));
        return new DrawingRectangle((int)corner.X, (int)corner.Y, window.Width, window.Height);
    }

    /// <summary>
    /// New top-left corner after the window changes size. The window stays attached to whichever
    /// horizontal and vertical screen edge it was closer to, then is kept inside <paramref name="bounds"/>.
    /// </summary>
    public static Point ResizeAnchored(Rect bounds, Point topLeft, Size oldSize, Size newSize)
    {
        var leftGap = topLeft.X - bounds.Left;
        var rightGap = bounds.Right - (topLeft.X + oldSize.Width);
        var topGap = topLeft.Y - bounds.Top;
        var bottomGap = bounds.Bottom - (topLeft.Y + oldSize.Height);

        var x = rightGap < leftGap ? topLeft.X + oldSize.Width - newSize.Width : topLeft.X;
        var y = bottomGap < topGap ? topLeft.Y + oldSize.Height - newSize.Height : topLeft.Y;

        return ClampTopLeft(bounds, new Point(x, y), newSize);
    }

    /// <summary>
    /// New top-left corner after a size change. The first time the widget grows from its empty
    /// placeholder to its real tiles, the corner stays where it was saved, since that position belongs
    /// to the full-size widget; later changes keep the screen edges the widget is closest to.
    /// </summary>
    public static Point AfterResize(Rect bounds, Point topLeft, Size oldSize, Size newSize, bool firstContent)
        => firstContent
            ? ClampTopLeft(bounds, topLeft, newSize)
            : ResizeAnchored(bounds, topLeft, oldSize, newSize);

    /// <summary>Same as <see cref="ClampInside"/> for a WPF top-left point and size.</summary>
    public static Point ClampTopLeft(Rect bounds, Point topLeft, Size size)
    {
        var x = Math.Clamp(topLeft.X, bounds.Left, Math.Max(bounds.Left, bounds.Right - size.Width));
        var y = Math.Clamp(topLeft.Y, bounds.Top, Math.Max(bounds.Top, bounds.Bottom - size.Height));
        return new Point(x, y);
    }

    public static Point CornerPosition(Rect area, Size size, ScreenCorner corner, double margin)
    {
        var x = corner is ScreenCorner.TopRight or ScreenCorner.BottomRight
            ? area.Right - size.Width - margin
            : area.Left + margin;
        var y = corner is ScreenCorner.BottomLeft or ScreenCorner.BottomRight
            ? area.Bottom - size.Height - margin
            : area.Top + margin;
        return new Point(x, y);
    }

    /// <summary>True when the top-left corner lies on the given screen area, with a small tolerance.</summary>
    public static bool IsReachable(Rect screens, Point topLeft, double tolerance = 40)
        => topLeft.X >= screens.Left - tolerance
           && topLeft.Y >= screens.Top - tolerance
           && topLeft.X < screens.Right - tolerance
           && topLeft.Y < screens.Bottom - tolerance;
}
