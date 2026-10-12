using System.Windows;
using System.Windows.Media;

namespace FrameHeat.Views;

/// <summary>An open 270-degree ring that fills clockwise from the bottom left.</summary>
internal sealed class ArcGauge : FrameworkElement
{
    private const double StartAngle = 135;
    private const double TotalSweep = 270;
    private const double MinVisibleSweep = 4;

    public static readonly DependencyProperty FractionProperty = DependencyProperty.Register(
        nameof(Fraction), typeof(double), typeof(ArcGauge),
        new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty ValueBrushProperty = DependencyProperty.Register(
        nameof(ValueBrush), typeof(Brush), typeof(ArcGauge),
        new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty TrackBrushProperty = DependencyProperty.Register(
        nameof(TrackBrush), typeof(Brush), typeof(ArcGauge),
        new FrameworkPropertyMetadata(Brushes.Gray, FrameworkPropertyMetadataOptions.AffectsRender));

    public static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register(
        nameof(Thickness), typeof(double), typeof(ArcGauge),
        new FrameworkPropertyMetadata(6.0, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>Filled part of the ring, 0 to 1.</summary>
    public double Fraction { get => (double)GetValue(FractionProperty); set => SetValue(FractionProperty, value); }

    public Brush ValueBrush { get => (Brush)GetValue(ValueBrushProperty); set => SetValue(ValueBrushProperty, value); }

    public Brush TrackBrush { get => (Brush)GetValue(TrackBrushProperty); set => SetValue(TrackBrushProperty, value); }

    public double Thickness { get => (double)GetValue(ThicknessProperty); set => SetValue(ThicknessProperty, value); }

    // Draws the grey track as a full 270-degree arc, then the colored part on top of it from the start.
    protected override void OnRender(DrawingContext context)
    {
        var size = Math.Min(ActualWidth, ActualHeight);
        if (size <= 0)
            return;

        var radius = size / 2 - Thickness / 2;
        var center = new Point(ActualWidth / 2, ActualHeight / 2);

        context.DrawGeometry(null, RoundPen(TrackBrush, Thickness), Arc(center, radius, TotalSweep));

        var fraction = Math.Clamp(Fraction, 0, 1);
        if (fraction > 0)
        {
            var sweep = Math.Max(TotalSweep * fraction, MinVisibleSweep);
            context.DrawGeometry(null, RoundPen(ValueBrush, Thickness), Arc(center, radius, sweep));
        }
    }

    private static Pen RoundPen(Brush brush, double thickness) => new(brush, thickness)
    {
        StartLineCap = PenLineCap.Round,
        EndLineCap = PenLineCap.Round,
    };

    // An arc from the start angle, sweepDegrees clockwise. Arcs over 180 degrees need the "large arc" flag.
    private static StreamGeometry Arc(Point center, double radius, double sweepDegrees)
    {
        var start = PointOnCircle(center, radius, StartAngle);
        var end = PointOnCircle(center, radius, StartAngle + sweepDegrees);

        var geometry = new StreamGeometry();
        using (var context = geometry.Open())
        {
            context.BeginFigure(start, isFilled: false, isClosed: false);
            context.ArcTo(end, new Size(radius, radius), 0, sweepDegrees > 180, SweepDirection.Clockwise, isStroked: true, isSmoothJoin: false);
        }

        geometry.Freeze();
        return geometry;
    }

    private static Point PointOnCircle(Point center, double radius, double degrees)
    {
        var radians = degrees * Math.PI / 180;
        return new Point(center.X + radius * Math.Cos(radians), center.Y + radius * Math.Sin(radians));
    }
}
