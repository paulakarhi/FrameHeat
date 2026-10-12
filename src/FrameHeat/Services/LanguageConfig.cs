using Microsoft.Win32;

namespace FrameHeat.Services;

/// <summary>
/// Finds the language chosen in the installer. The installer stores it in the registry; without an
/// installer there is no value and the Windows UI language is used instead.
/// </summary>
internal static class LanguageConfig
{
    /// <summary>The language code to use: the command line first, then the installer's choice; null means "follow Windows".</summary>
    public static string? Read(string? commandLineOverride)
    {
        if (!string.IsNullOrWhiteSpace(commandLineOverride))
            return commandLineOverride;

        // The installer writes to the 64-bit registry view.
        foreach (var hive in new[] { RegistryHive.LocalMachine, RegistryHive.CurrentUser })
        {
            using var root = RegistryKey.OpenBaseKey(hive, RegistryView.Registry64);
            using var key = root.OpenSubKey(AppIdentity.RegistryKeyPath);
            if (key?.GetValue(AppIdentity.LanguageValueName) is string { Length: > 0 } value)
                return value;
        }

        return null;
    }
}
