using System.Globalization;
using System.Windows.Media;

namespace FrameHeat.ViewModels;

/// <summary>
/// Everything one tile shows: texts, colors and visibility flags, derived from the latest
/// reading, the alert limits and the appearance settings.
/// </summary>
internal sealed class MetricViewModel(string key) : ObservableObject
{
    // The gauge ring starts here, so a cool device does not look half full.
    private const double GaugeFloorC = 20;

    // The ring of a frame rate is full at the monitor's refresh rate; this is used when that is unknown.
    private const double DefaultRefreshRateHz = 144;

    // Shown in place of a number while the device has no reading.
    private const string NoValue = "–";

    // Each tracker remembers the previous severity, which is what keeps a value at a limit from flickering.
    private readonly SeverityTracker _temperatureTracker = new();
    private readonly SeverityTracker _usageTracker = new();
    private MetricReading? _reading;

    // Backing fields for the properties the tile binds to.
    private string _label = "";
    private string _iconData = "";
    private bool _showLabel = true;
    private TileDesign _design = TileDesign.Gauge;
    private TileShape _shape = TileShape.From(new AppearanceSettings());
    private double _textScale = 1.0;
    private string _primaryText = NoValue;
    private string _primaryUnit = "";
    private Brush _primaryBrush = Palette.Unknown;
    private double _gaugeFraction;
    private Brush _gaugeBrush = Palette.Unknown;
    private string _secondaryText = "";
    private bool _showSecondary;
    private string _extraText = "";
    private double _usageFraction;
    private bool _showBar;
    private Brush _usageBrush = Palette.Text;
    private Severity _severity = Severity.Unknown;
    private Brush _tileBackground = Brushes.Transparent;
    private Brush _tileOutline = Brushes.Transparent;
    private Brush _tint = Brushes.Transparent;

    /// <summary>Identifies the device, so the same tile is reused when its reading changes.</summary>
    public string Key { get; } = key;

    // Everything below is display state, set only by Recompute.
    public string Label { get => _label; private set => Set(ref _label, value); }
    public string IconData { get => _iconData; private set => Set(ref _iconData, value); }
    public bool ShowLabel { get => _showLabel; private set => Set(ref _showLabel, value); }

    public TileDesign Design
    {
        get => _design;
        private set
        {
            if (!Set(ref _design, value))
                return;

            OnPropertyChanged(nameof(IsGauge));
            OnPropertyChanged(nameof(IsCompact));
            OnPropertyChanged(nameof(IsTextRow));
            OnPropertyChanged(nameof(IsCardDesign));
        }
    }

    public TileShape Shape { get => _shape; private set => Set(ref _shape, value); }

    /// <summary>Multiplies the size of every text in the tile.</summary>
    public double TextScale { get => _textScale; private set => Set(ref _textScale, value); }

    public bool IsGauge => _design == TileDesign.Gauge;
    public bool IsCompact => _design == TileDesign.Compact;
    public bool IsTextRow => _design == TileDesign.TextRow;

    /// <summary>The designs that draw a tile card, as opposed to a bare line of text.</summary>
    public bool IsCardDesign => !IsTextRow;

    public string PrimaryText { get => _primaryText; private set => Set(ref _primaryText, value); }
    public string PrimaryUnit { get => _primaryUnit; private set => Set(ref _primaryUnit, value); }
    public Brush PrimaryBrush { get => _primaryBrush; private set => Set(ref _primaryBrush, value); }
    public double GaugeFraction { get => _gaugeFraction; private set => Set(ref _gaugeFraction, value); }
    public Brush GaugeBrush { get => _gaugeBrush; private set => Set(ref _gaugeBrush, value); }
    public string SecondaryText { get => _secondaryText; private set => Set(ref _secondaryText, value); }
    public bool ShowSecondary { get => _showSecondary; private set => Set(ref _showSecondary, value); }

    /// <summary>Small lines under the value, one per line. Only a frame rate has any.</summary>
    public string ExtraText
    {
        get => _extraText;
        private set
        {
            if (Set(ref _extraText, value))
                OnPropertyChanged(nameof(ShowExtra));
        }
    }

    public bool ShowExtra => _extraText.Length > 0;
    public double UsageFraction { get => _usageFraction; private set => Set(ref _usageFraction, value); }
    public bool ShowBar { get => _showBar; private set => Set(ref _showBar, value); }
    public Brush UsageBrush { get => _usageBrush; private set => Set(ref _usageBrush, value); }
    public Severity Severity { get => _severity; private set => Set(ref _severity, value); }
    public Brush TileBackground { get => _tileBackground; private set => Set(ref _tileBackground, value); }
    public Brush TileOutline { get => _tileOutline; private set => Set(ref _tileOutline, value); }
    public Brush Tint { get => _tint; private set => Set(ref _tint, value); }

    /// <summary>Takes a new reading and refreshes what the tile shows.</summary>
    public void Apply(MetricReading reading, AppSettings settings)
    {
        _reading = reading;
        Recompute(settings);
    }

    /// <summary>Recomputes the tile from the last reading, for example after a settings change.</summary>
    public void Recompute(AppSettings settings)
    {
        if (_reading is not { } reading)
            return;

        var appearance = settings.Appearance;
        var colors = Palette.AlertColors(appearance);

        Label = MetricLabels.For(reading);
        IconData = MetricIcons.For(reading.Kind);
        ShowLabel = appearance.ShowLabels;
        Design = appearance.Design;
        Shape = TileShape.From(appearance);
        TextScale = appearance.TextScale;

        // The tile border and tint follow the worse of temperature and usage.
        Severity = reading.Kind == MetricKind.Fps
            ? ShowFrameRate(reading.Frames, appearance, colors)
            : ShowDevice(reading, settings, colors);

        TileBackground = Palette.TileBackground(appearance.PanelStyle, appearance.BackgroundOpacity);
        TileOutline = colors.Outline(Severity);
        Tint = colors.Tint(Severity);
    }

    // A temperature and a load. Returns how bad the tile looks as a whole.
    private Severity ShowDevice(MetricReading reading, AppSettings settings, SeverityColors colors)
    {
        var appearance = settings.Appearance;

        // Disks can show how full they are or how busy they are right now; every other device shows its load.
        var usage = reading.Kind == MetricKind.Disk && appearance.DiskUsage == DiskUsageMode.Activity
            ? reading.ActivityPercent
            : reading.UsagePercent;

        // How hot is too hot: the device's own limit when it reports one, otherwise the configured limits.
        var limits = LimitResolver.Resolve(settings.Alerts.For(reading.Kind), reading.DeviceLimitC, settings.Alerts.UseDeviceLimits);
        var temperatureSeverity = _temperatureTracker.Update(reading.TemperatureC, limits.Warning, limits.Critical);
        var usageSeverity = EvaluateUsage(reading.Kind, usage, appearance.DiskUsage);

        ApplyPrimary(reading.TemperatureC, usage, temperatureSeverity, usageSeverity, limits.Critical, appearance, colors);
        ApplyUsage(usage, usageSeverity, hasTemperature: reading.TemperatureC is not null, appearance, colors);
        ExtraText = "";

        return temperatureSeverity > usageSeverity ? temperatureSeverity : usageSeverity;
    }

    // Frames per second as the headline, the time between frames beside the label and the one percent
    // low and the monitor's refresh rate as small lines. A frame rate has no alert limits.
    private Severity ShowFrameRate(FrameStats? frames, AppearanceSettings appearance, SeverityColors colors)
    {
        var severity = frames is null ? Severity.Unknown : Severity.Normal;

        PrimaryText = frames is null ? NoValue : NumberText.Whole(frames.FramesPerSecond);
        PrimaryUnit = frames is null ? "" : "FPS";
        GaugeFraction = frames is null ? 0 : Math.Clamp(frames.FramesPerSecond / (frames.RefreshRateHz ?? DefaultRefreshRateHz), 0, 1);
        ShowPrimaryColors(severity, appearance, colors);

        // The only number written with the user's own decimal separator; whole numbers have none.
        SecondaryText = frames is null ? "" : $"{frames.FrameTimeMs.ToString("0.0", CultureInfo.CurrentCulture)} ms";
        ShowSecondary = frames is not null;
        ShowBar = false;
        UsageFraction = 0;
        ExtraText = frames is null ? "" : string.Join("\n", ExtraLines(frames));

        return severity;
    }

    private static IEnumerable<string> ExtraLines(FrameStats frames)
    {
        if (frames.OnePercentLowFps is { } low)
            yield return $"{Loc.T("fps_low")} {NumberText.Whole(low)}";

        if (frames.RefreshRateHz is { } hertz)
            yield return $"{Loc.T("fps_display")} {NumberText.Whole(hertz)} Hz";
    }

    // Only memory and disk fullness raise an alert; CPU and GPU load is never a warning in itself.
    private Severity EvaluateUsage(MetricKind kind, double? usage, DiskUsageMode diskMode)
    {
        if (usage is null)
            return Severity.Unknown;

        return DefaultLimits.ForUsage(kind, diskMode) is { } limits
            ? _usageTracker.Update(usage, limits.Warning, limits.Critical)
            : Severity.Normal;
    }

    // The headline value is the temperature. When there is none (memory without a sensor, or no
    // driver) the usage is shown instead; the unit (% rather than °C) tells which one it is.
    private void ApplyPrimary(double? temperature, double? usage, Severity temperatureSeverity, Severity usageSeverity,
        double criticalLimit, AppearanceSettings appearance, SeverityColors colors)
    {
        Severity severity;

        if (temperature is { } celsius)
        {
            PrimaryText = NumberText.Whole(Temperature.Convert(celsius, appearance.TemperatureUnit));
            PrimaryUnit = Temperature.Symbol(appearance.TemperatureUnit);
            severity = temperatureSeverity;
            GaugeFraction = Math.Clamp((celsius - GaugeFloorC) / (criticalLimit - GaugeFloorC), 0, 1);
        }
        else if (usage is { } percent)
        {
            PrimaryText = NumberText.Whole(percent);
            PrimaryUnit = "%";
            severity = usageSeverity == Severity.Unknown ? Severity.Normal : usageSeverity;
            GaugeFraction = Math.Clamp(percent / 100.0, 0, 1);
        }
        else
        {
            PrimaryText = NoValue;
            PrimaryUnit = "";
            severity = Severity.Unknown;
            GaugeFraction = 0;
        }

        ShowPrimaryColors(severity, appearance, colors);
    }

    private void ShowPrimaryColors(Severity severity, AppearanceSettings appearance, SeverityColors colors)
    {
        PrimaryBrush = colors.ForSeverity(severity);
        GaugeBrush = colors.Gauge(severity, Palette.Accent(appearance.AccentColor));
    }

    // The usage figure is a small number beside the label and a bar under the gauge. When there is no
    // temperature the usage becomes the headline value, so only the bar is left for it here.
    private void ApplyUsage(double? usage, Severity usageSeverity, bool hasTemperature, AppearanceSettings appearance, SeverityColors colors)
    {
        ShowSecondary = hasTemperature && usage is not null && appearance.ShowUsage;
        SecondaryText = usage is { } percent ? $"{NumberText.Whole(percent)} %" : "";

        ShowBar = usage is not null && (appearance.ShowUsage || !hasTemperature);
        UsageFraction = Math.Clamp((usage ?? 0) / 100.0, 0, 1);
        UsageBrush = usageSeverity is Severity.Warning or Severity.Critical
            ? colors.ForSeverity(usageSeverity)
            : Palette.Accent(appearance.AccentColor);
    }
}
