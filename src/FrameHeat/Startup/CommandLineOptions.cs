using FrameHeat.Services;

namespace FrameHeat.Startup;

/// <summary>Command-line switches. Most exist for development and testing.</summary>
internal sealed record CommandLineOptions
{
    /// <summary>Write a hardware report to this file and exit, without opening any window.</summary>
    public string? DumpPath { get; init; }

    /// <summary>Use this settings file instead of the default one.</summary>
    public string? SettingsFile { get; init; }

    /// <summary>Force a UI language (en, fi, sv, de, fr, es, pt, zh, ja, ko).</summary>
    public string? Language { get; init; }

    /// <summary>Show synthetic readings instead of real sensors.</summary>
    public bool Demo { get; init; }

    public bool EditMode { get; init; }

    public bool OpenSettings { get; init; }

    public bool AllowMultipleInstances { get; init; }

    /// <summary>Started by the scheduled task at logon.</summary>
    public bool AutoStarted { get; init; }

    /// <summary>Reads the switches; unknown ones are ignored so a stray argument never stops the program.</summary>
    public static CommandLineOptions Parse(IReadOnlyList<string> args)
    {
        string? dumpPath = null;
        string? settingsFile = null;
        string? language = null;
        bool demo = false, editMode = false, openSettings = false, multiple = false, autoStarted = false;

        for (var i = 0; i < args.Count; i++)
        {
            switch (args[i].ToLowerInvariant())
            {
                case "--dump": dumpPath = ValueAfter(args, ref i); break;
                case "--settings-file": settingsFile = ValueAfter(args, ref i); break;
                case "--lang": language = ValueAfter(args, ref i); break;
                case "--demo": demo = true; break;
                case "--edit": editMode = true; break;
                case "--settings": openSettings = true; break;
                case "--multi": multiple = true; break;
                case AutoStartTask.AutoStartArgument: autoStarted = true; break;
            }
        }

        return new CommandLineOptions
        {
            DumpPath = dumpPath,
            SettingsFile = settingsFile,
            Language = language,
            Demo = demo,
            EditMode = editMode,
            OpenSettings = openSettings,
            AllowMultipleInstances = multiple,
            AutoStarted = autoStarted,
        };
    }

    // A value is the next token unless that is missing or looks like another switch.
    private static string? ValueAfter(IReadOnlyList<string> args, ref int index)
    {
        if (index + 1 >= args.Count || args[index + 1].StartsWith("--", StringComparison.Ordinal))
            return null;

        return args[++index];
    }
}
