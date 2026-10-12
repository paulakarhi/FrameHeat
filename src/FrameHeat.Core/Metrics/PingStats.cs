namespace FrameHeat.Core.Metrics;

/// <summary>How long a small test message takes to reach an address and come back.</summary>
/// <param name="LastMs">The latest round trip; null when the latest message got no reply.</param>
/// <param name="AverageMs">The average of the replies among the latest messages; null when there were none.</param>
/// <param name="LossPercent">The share of the latest messages that got no reply.</param>
public sealed record PingStats(double? LastMs, double? AverageMs, double LossPercent);
