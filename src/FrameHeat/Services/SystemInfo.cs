using System.Security.Principal;
using Microsoft.Win32;

namespace FrameHeat.Services;

/// <summary>Facts about the machine and the process that explain which sensors can be read.</summary>
internal static class SystemInfo
{
    // A process cannot gain or lose administrator rights while it runs.
    private static readonly Lazy<bool> Elevated = new(ReadElevation);

    public static bool IsElevated() => Elevated.Value;

    /// <summary>CPU and memory temperatures are read through the PawnIO driver.</summary>
    public static bool IsPawnIoInstalled()
    {
        using var key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\PawnIO");
        return key is not null;
    }

    private static bool ReadElevation()
    {
        using var identity = WindowsIdentity.GetCurrent();
        return new WindowsPrincipal(identity).IsInRole(WindowsBuiltInRole.Administrator);
    }
}
