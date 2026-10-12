namespace FrameHeat.Core.Metrics;

/// <summary>How a game is rendering at the moment.</summary>
/// <param name="FramesPerSecond">Frames the game produced over the last second.</param>
/// <param name="FrameTimeMs">Average time between two frames over the same second.</param>
/// <param name="OnePercentLowFps">
/// Frame rate of the slowest one percent of frames over the last few seconds. Missing until enough
/// frames have been seen to make the figure meaningful.
/// </param>
/// <param name="RefreshRateHz">Refresh rate of the monitor the game runs on, when it could be read.</param>
public sealed record FrameStats(double FramesPerSecond, double FrameTimeMs, double? OnePercentLowFps, double? RefreshRateHz);
