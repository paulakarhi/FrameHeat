using System.Globalization;
using System.Text;

namespace FrameHeat.Services;

/// <summary>
/// Appends lines to a text file and moves it aside to <c>.old</c> once it grows past a size limit.
/// Logging must never take the application down, so every failure is swallowed.
/// </summary>
internal sealed class LogFile(string path, long maxBytes)
{
    private readonly object _gate = new();

    /// <summary>Appends one timestamped line. Safe to call from any thread.</summary>
    public void Write(string level, string message)
    {
        try
        {
            lock (_gate)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                RotateIfLarge();

                var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                var line = $"{timestamp} {level} {message}{Environment.NewLine}";
                File.AppendAllText(path, line, Encoding.UTF8);
            }
        }
        catch (Exception)
        {
            // Nothing sensible can be done about a failing log.
        }
    }

    private void RotateIfLarge()
    {
        if (!File.Exists(path) || new FileInfo(path).Length <= maxBytes)
            return;

        File.Move(path, path + ".old", overwrite: true);
    }
}
