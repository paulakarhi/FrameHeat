using System.Globalization;
using FrameHeat.Core.Localization.Translations;

namespace FrameHeat.Core.Localization;

/// <summary>
/// UI text lookup. The language is chosen once at startup (<see cref="Use"/>), so texts are
/// fetched directly by key and never change while the program runs.
/// </summary>
public static class Loc
{
    /// <summary>Supported languages as (code, name in that language), in installer order.</summary>
    public static readonly IReadOnlyList<(string Code, string NativeName)> Languages =
    [
        ("en", "English"),
        ("fi", "Suomi"),
        ("sv", "Svenska"),
        ("de", "Deutsch"),
        ("fr", "Français"),
        ("es", "Español"),
        ("pt", "Português (Brasil)"),
        ("zh", "中文（简体）"),
        ("ja", "日本語"),
        ("ko", "한국어"),
    ];

    private static IReadOnlyDictionary<string, string> _current = English.Strings;

    /// <summary>
    /// Selects the language. <paramref name="configured"/> is the installer's choice (for example
    /// "fi" or "pt-BR"); without one the Windows UI language is used, and English if that is unsupported.
    /// </summary>
    public static void Use(string? configured, CultureInfo? system = null)
    {
        _current = TableFor(Resolve(configured, system));
    }

    public static string Resolve(string? configured, CultureInfo? system = null)
    {
        if (Normalize(configured) is { } chosen && IsSupported(chosen))
            return chosen;

        if (Normalize((system ?? CultureInfo.CurrentUICulture).Name) is { } fromSystem && IsSupported(fromSystem))
            return fromSystem;

        return "en";
    }

    /// <summary>Text for a key. A missing translation falls back to English, then to the key itself.</summary>
    public static string T(string key)
        => _current.TryGetValue(key, out var text) ? text
         : English.Strings.TryGetValue(key, out var fallback) ? fallback
         : key;

    public static IReadOnlyDictionary<string, string> TableFor(string code) => code switch
    {
        "fi" => Finnish.Strings,
        "sv" => Swedish.Strings,
        "de" => German.Strings,
        "fr" => French.Strings,
        "es" => Spanish.Strings,
        "pt" => Portuguese.Strings,
        "zh" => Chinese.Strings,
        "ja" => Japanese.Strings,
        "ko" => Korean.Strings,
        _ => English.Strings,
    };

    private static bool IsSupported(string code) => Languages.Any(language => language.Code == code);

    // "pt-BR" -> "pt", "zh_CN" -> "zh", " FI " -> "fi"
    private static string? Normalize(string? code)
    {
        if (string.IsNullOrWhiteSpace(code))
            return null;

        var trimmed = code.Trim();
        var separator = trimmed.IndexOfAny(['-', '_']);
        return (separator < 0 ? trimmed : trimmed[..separator]).ToLowerInvariant();
    }
}
