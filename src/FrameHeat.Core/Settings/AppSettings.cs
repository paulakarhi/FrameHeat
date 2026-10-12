using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FrameHeat.Core.Settings;

/// <summary>
/// Everything the user can change. Defaults make the application usable without touching anything.
/// <see cref="Changed"/> fires for a change in any section.
/// </summary>
public sealed class AppSettings : ObservableObject
{
    /// <summary>The address the ping tile measures the delay to until the user chooses another.</summary>
    public const string DefaultPingTarget = "1.1.1.1";

    private AppearanceSettings _appearance = new();
    private TileVisibility _tiles = new();
    private AlertSettings _alerts = new();
    private WindowPlacement _placement = new();
    private bool _startWithWindows;
    private bool _alwaysOnTop = true;
    private string _pingTarget = DefaultPingTarget;

    // Changes in any section are forwarded as one Changed event, so a listener needs only one subscription.
    public AppSettings()
    {
        _appearance.PropertyChanged += OnSectionChanged;
        _tiles.PropertyChanged += OnSectionChanged;
        _alerts.PropertyChanged += OnSectionChanged;
        _placement.PropertyChanged += OnSectionChanged;
    }

    public event Action<SettingsChange>? Changed;

    public AppearanceSettings Appearance { get => _appearance; set => SetSection(ref _appearance, value); }
    public TileVisibility Tiles { get => _tiles; set => SetSection(ref _tiles, value); }
    public AlertSettings Alerts { get => _alerts; set => SetSection(ref _alerts, value); }
    public WindowPlacement Placement { get => _placement; set => SetSection(ref _placement, value); }

    public bool StartWithWindows { get => _startWithWindows; set => SetGeneral(ref _startWithWindows, value); }

    /// <summary>Keep the widget above games and other windows. When off it is an ordinary window that a game in front of it hides.</summary>
    public bool AlwaysOnTop { get => _alwaysOnTop; set => SetGeneral(ref _alwaysOnTop, value); }

    /// <summary>The address the ping tile measures the delay to: an IP address or a host name. Anything else is ignored.</summary>
    public string PingTarget
    {
        get => _pingTarget;
        set
        {
            var address = value?.Trim();
            if (IsValidPingTarget(address))
                SetGeneral(ref _pingTarget, address!);
        }
    }

    /// <summary>True for an IP address or a host name of a sensible length.</summary>
    public static bool IsValidPingTarget(string? value)
        => !string.IsNullOrWhiteSpace(value) && value.Length <= 253
           && Uri.CheckHostName(value) is UriHostNameType.Dns or UriHostNameType.IPv4 or UriHostNameType.IPv6;

    // A setting that belongs to no section is reported as a general change.
    private void SetGeneral<T>(ref T field, T value, [CallerMemberName] string? name = null)
    {
        if (Set(ref field, value, name))
            Changed?.Invoke(new SettingsChange(SettingsArea.General, name));
    }

    // Replaces a whole section (for example when loading from file) and reports it like any other change.
    private void SetSection<T>(ref T field, T? value, [CallerMemberName] string? name = null)
        where T : ObservableObject, new()
    {
        if (SetChild(ref field, value, OnSectionChanged, name))
            Changed?.Invoke(new SettingsChange(AreaOf(field), name));
    }

    private void OnSectionChanged(object? sender, PropertyChangedEventArgs e)
        => Changed?.Invoke(new SettingsChange(AreaOf(sender), e.PropertyName));

    private SettingsArea AreaOf(object? section)
        => ReferenceEquals(section, _appearance) ? SettingsArea.Appearance
         : ReferenceEquals(section, _tiles) ? SettingsArea.Tiles
         : ReferenceEquals(section, _alerts) ? SettingsArea.Alerts
         : SettingsArea.Placement;
}
