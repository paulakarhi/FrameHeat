namespace FrameHeat.Services;

/// <summary>Application log: a text file in the local app data folder, for finding out what went wrong afterwards.</summary>
internal static class Log
{
    private static readonly LogFile Target = new(Path.Combine(AppIdentity.LogDirectory, "frameheat.log"), maxBytes: 256 * 1024);

    public static void Info(string message) => Target.Write("INFO ", message);

    public static void Error(string message, Exception? exception = null)
        => Target.Write("ERROR", exception is null ? message : $"{message}: {exception}");
}
