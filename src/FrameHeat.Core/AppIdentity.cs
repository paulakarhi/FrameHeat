namespace FrameHeat.Core;

/// <summary>Names the application registers under with Windows and on disk.</summary>
public static class AppIdentity
{
    public const string Name = "FrameHeat";
    public const string ScheduledTaskName = Name;
    public const string SingleInstanceMutex = @"Local\FrameHeat.SingleInstance";
    public const string RegistryKeyPath = @"Software\FrameHeat";
    public const string LanguageValueName = "Language";

    public static string DataDirectory => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Name);

    public static string LogDirectory => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Name);
}
