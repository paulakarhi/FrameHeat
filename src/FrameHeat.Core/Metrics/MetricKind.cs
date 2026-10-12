namespace FrameHeat.Core.Metrics;

/// <summary>The kinds of device that get a tile. Every drive is a <see cref="Disk"/>; <see cref="Vram"/> is the graphics card's memory.</summary>
public enum MetricKind
{
    Cpu,
    Gpu,
    Vram,
    Memory,
    Disk,

    /// <summary>The frame rate of the game in the foreground. It has no temperature or load.</summary>
    Fps,

    /// <summary>How fast data moves through the network connection in use. It has no temperature or load.</summary>
    Network,

    /// <summary>How long a test message takes to reach an address chosen by the user and come back.</summary>
    Ping,
}
