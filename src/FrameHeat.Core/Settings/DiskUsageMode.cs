namespace FrameHeat.Core.Settings;

/// <summary>Which percentage a disk tile shows beside its temperature.</summary>
public enum DiskUsageMode
{
    /// <summary>How full the drive is.</summary>
    UsedSpace,

    /// <summary>How busy the drive is right now.</summary>
    Activity,
}
