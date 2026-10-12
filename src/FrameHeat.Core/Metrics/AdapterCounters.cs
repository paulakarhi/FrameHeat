namespace FrameHeat.Core.Metrics;

/// <summary>The bytes one network adapter has sent and received since Windows started counting.</summary>
public sealed record AdapterCounters(string Id, ulong BytesReceived, ulong BytesSent, double? LinkSpeedBitsPerSecond);
