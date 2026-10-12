using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace FrameHeat.Core.Settings;

/// <summary>How the widget looks. Out-of-range values are corrected, not rejected.</summary>
public sealed partial class AppearanceSettings : ObservableObject
{
    public const double MinScale = 0.6;
    public const double MaxScale = 2.0;
    public const double MinBackgroundOpacity = 0.0;
    public const double MaxBackgroundOpacity = 1.0;
    public const double MinCornerRadius = 0;
    public const double MaxCornerRadius = 24;
    public const double MinTileSpacing = 0;
    public const double MaxTileSpacing = 16;
    public const double MinTextScale = 0.8;
    public const double MaxTextScale = 1.5;
    public const double MinWidgetOpacity = 0.3;
    public const double MaxWidgetOpacity = 1.0;

    /// <summary>The accent colors the settings window offers. The first one is the default.</summary>
    public static IReadOnlyList<string> AccentChoices { get; } =
        ["#4CC9F0", "#4ADE80", "#A78BFA", "#FB923C", "#F472B6", "#E5E7EB"];

    public static string DefaultAccentColor => AccentChoices[0];

    public const string DefaultWarningColor = "#FFB627";
    public const string DefaultCriticalColor = "#FF4D4D";

    private TileDesign _design = TileDesign.Gauge;
    private PanelOrientation _orientation = PanelOrientation.Horizontal;
    private PanelStyle _panelStyle = PanelStyle.Glass;
    private TemperatureUnit _temperatureUnit = TemperatureUnit.Celsius;
    private double _scale = 1.0;
    private double _backgroundOpacity = 0.85;
    private double _cornerRadius = 13;
    private double _tileSpacing = 6;
    private double _textScale = 1.0;
    private double _widgetOpacity = 1.0;
    private string _accentColor = DefaultAccentColor;
    private string _warningColor = DefaultWarningColor;
    private string _criticalColor = DefaultCriticalColor;
    private bool _showLabels = true;
    private bool _showUsage = true;
    private DiskUsageMode _diskUsage = DiskUsageMode.UsedSpace;

    public TileDesign Design { get => _design; set => Set(ref _design, value); }
    public PanelOrientation Orientation { get => _orientation; set => Set(ref _orientation, value); }
    public PanelStyle PanelStyle { get => _panelStyle; set => Set(ref _panelStyle, value); }
    public TemperatureUnit TemperatureUnit { get => _temperatureUnit; set => Set(ref _temperatureUnit, value); }

    public double Scale { get => _scale; set => SetClamped(ref _scale, value, MinScale, MaxScale); }

    /// <summary>0 is fully transparent, 1 fully opaque. Applies to the glass panel or, without a panel, to the tiles.</summary>
    public double BackgroundOpacity { get => _backgroundOpacity; set => SetClamped(ref _backgroundOpacity, value, MinBackgroundOpacity, MaxBackgroundOpacity); }

    /// <summary>Roundness of the tile corners; the panel around them follows.</summary>
    public double CornerRadius { get => _cornerRadius; set => SetClamped(ref _cornerRadius, value, MinCornerRadius, MaxCornerRadius); }

    /// <summary>Gap between two tiles.</summary>
    public double TileSpacing { get => _tileSpacing; set => SetClamped(ref _tileSpacing, value, MinTileSpacing, MaxTileSpacing); }

    /// <summary>Size of the text relative to the tiles, so text can grow without the tiles growing with it.</summary>
    public double TextScale { get => _textScale; set => SetClamped(ref _textScale, value, MinTextScale, MaxTextScale); }

    /// <summary>Fades the whole widget, text included, unlike <see cref="BackgroundOpacity"/>.</summary>
    public double WidgetOpacity { get => _widgetOpacity; set => SetClamped(ref _widgetOpacity, value, MinWidgetOpacity, MaxWidgetOpacity); }

    /// <summary>Colors as #RRGGBB or #AARRGGBB. Invalid values are ignored.</summary>
    public string AccentColor { get => _accentColor; set => SetColor(ref _accentColor, value); }
    public string WarningColor { get => _warningColor; set => SetColor(ref _warningColor, value); }
    public string CriticalColor { get => _criticalColor; set => SetColor(ref _criticalColor, value); }

    public bool ShowLabels { get => _showLabels; set => Set(ref _showLabels, value); }
    public bool ShowUsage { get => _showUsage; set => Set(ref _showUsage, value); }
    public DiskUsageMode DiskUsage { get => _diskUsage; set => Set(ref _diskUsage, value); }

    public void ToggleOrientation()
        => Orientation = Orientation == PanelOrientation.Horizontal ? PanelOrientation.Vertical : PanelOrientation.Horizontal;

    public void ResetAlertColors()
    {
        WarningColor = DefaultWarningColor;
        CriticalColor = DefaultCriticalColor;
    }

    public static bool IsValidColor(string? value) => value is not null && HexColor().IsMatch(value);

    private void SetClamped(ref double field, double value, double min, double max, [CallerMemberName] string? name = null)
    {
        if (!double.IsNaN(value))
            Set(ref field, Math.Clamp(value, min, max), name);
    }

    private void SetColor(ref string field, string? value, [CallerMemberName] string? name = null)
    {
        if (IsValidColor(value))
            Set(ref field, value!, name);
    }

    [GeneratedRegex("^#([0-9A-Fa-f]{6}|[0-9A-Fa-f]{8})$")]
    private static partial Regex HexColor();
}
