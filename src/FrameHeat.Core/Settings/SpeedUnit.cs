namespace FrameHeat.Core.Settings;

/// <summary>Whether network speeds are shown in bits or in bytes. The size, such as Mbit/s or GB/s, follows the speed.</summary>
public enum SpeedUnit
{
    /// <summary>Bits per second, as connections are usually sold.</summary>
    MegabitsPerSecond,

    /// <summary>Bytes per second, as file transfers are usually shown.</summary>
    MegabytesPerSecond,
}
