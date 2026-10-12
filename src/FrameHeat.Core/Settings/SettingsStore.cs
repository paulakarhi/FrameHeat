using System.Text.Json;
using System.Text.Json.Serialization;

namespace FrameHeat.Core.Settings;

/// <summary>Reads and writes <see cref="AppSettings"/> as a JSON file.</summary>
public sealed class SettingsStore
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
        Converters = { new JsonStringEnumConverter(allowIntegerValues: false) },
    };

    public SettingsStore(string filePath) => FilePath = filePath;

    public static string DefaultPath => Path.Combine(AppIdentity.DataDirectory, "settings.json");

    public string FilePath { get; }

    public bool Exists => File.Exists(FilePath);

    /// <summary>
    /// Loads the settings. A missing file yields defaults; an unreadable one is moved to
    /// <c>.bak</c> and replaced by defaults so it can never block startup.
    /// </summary>
    public AppSettings Load()
    {
        if (!File.Exists(FilePath))
            return new AppSettings();

        try
        {
            using var stream = File.OpenRead(FilePath);
            return JsonSerializer.Deserialize<AppSettings>(stream, JsonOptions) ?? new AppSettings();
        }
        catch (Exception ex) when (ex is JsonException or NotSupportedException or IOException or UnauthorizedAccessException)
        {
            MoveAside();
            return new AppSettings();
        }
    }

    public void Save(AppSettings settings)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);

        // Write to a temp file first so an interrupted write cannot corrupt the existing settings.
        var temp = FilePath + ".tmp";
        File.WriteAllText(temp, JsonSerializer.Serialize(settings, JsonOptions));
        File.Move(temp, FilePath, overwrite: true);
    }

    private void MoveAside()
    {
        try
        {
            File.Move(FilePath, FilePath + ".bak", overwrite: true);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            // Nothing more to do; startup continues with defaults.
        }
    }
}
