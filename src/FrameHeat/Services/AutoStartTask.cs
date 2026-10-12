using System.Diagnostics;
using System.Security;
using System.Security.Principal;
using System.Text;

namespace FrameHeat.Services;

/// <summary>
/// Starts the application at logon through a scheduled task. Unlike a startup shortcut, a task
/// can run elevated without a UAC prompt on every boot.
/// </summary>
internal static class AutoStartTask
{
    /// <summary>Passed by the task so the program knows Windows started it and skips the startup hint.</summary>
    public const string AutoStartArgument = "--autostart";

    private static readonly TimeSpan SchtasksTimeout = TimeSpan.FromSeconds(15);

    /// <summary>True when the logon task exists.</summary>
    public static bool IsEnabled() => RunSchtasks("/Query", "/TN", AppIdentity.ScheduledTaskName) == 0;

    /// <summary>Creates or replaces the task for the running executable. Requires administrator rights.</summary>
    public static bool Enable()
    {
        var executable = Environment.ProcessPath;
        if (string.IsNullOrEmpty(executable) || IsDotnetHost(executable))
            return false;

        var xmlPath = Path.Combine(Path.GetTempPath(), $"{AppIdentity.Name}-{Path.GetRandomFileName()}.xml");
        try
        {
            File.WriteAllText(xmlPath, BuildTaskXml(executable, WindowsIdentity.GetCurrent().Name), Encoding.Unicode);
            return RunSchtasks("/Create", "/TN", AppIdentity.ScheduledTaskName, "/XML", xmlPath, "/F") == 0;
        }
        finally
        {
            try
            {
                File.Delete(xmlPath);
            }
            catch (IOException)
            {
                // A leftover file in the temp folder does no harm.
            }
        }
    }

    /// <summary>Removes the task.</summary>
    public static bool Disable() => RunSchtasks("/Delete", "/TN", AppIdentity.ScheduledTaskName, "/F") == 0;

    // The task definition handed to schtasks: start the program elevated, for this user, ten seconds after logon.
    internal static string BuildTaskXml(string executablePath, string userName)
    {
        var user = SecurityElement.Escape(userName);
        var command = SecurityElement.Escape(executablePath);
        var workingDirectory = SecurityElement.Escape(Path.GetDirectoryName(executablePath) ?? "");

        // Defaults would stop the task after 72 hours and refuse to start on battery power.
        return $"""
            <?xml version="1.0" encoding="UTF-16"?>
            <Task version="1.4" xmlns="http://schemas.microsoft.com/windows/2004/02/mit/task">
              <Triggers>
                <LogonTrigger>
                  <Enabled>true</Enabled>
                  <UserId>{user}</UserId>
                  <Delay>PT10S</Delay>
                </LogonTrigger>
              </Triggers>
              <Principals>
                <Principal id="Author">
                  <UserId>{user}</UserId>
                  <LogonType>InteractiveToken</LogonType>
                  <RunLevel>HighestAvailable</RunLevel>
                </Principal>
              </Principals>
              <Settings>
                <MultipleInstancesPolicy>IgnoreNew</MultipleInstancesPolicy>
                <DisallowStartIfOnBatteries>false</DisallowStartIfOnBatteries>
                <StopIfGoingOnBatteries>false</StopIfGoingOnBatteries>
                <ExecutionTimeLimit>PT0S</ExecutionTimeLimit>
                <StartWhenAvailable>true</StartWhenAvailable>
                <Enabled>true</Enabled>
              </Settings>
              <Actions Context="Author">
                <Exec>
                  <Command>{command}</Command>
                  <Arguments>{AutoStartArgument}</Arguments>
                  <WorkingDirectory>{workingDirectory}</WorkingDirectory>
                </Exec>
              </Actions>
            </Task>
            """;
    }

    // Running through "dotnet run" would register the dotnet host instead of this program.
    private static bool IsDotnetHost(string executable)
        => Path.GetFileNameWithoutExtension(executable).Equals("dotnet", StringComparison.OrdinalIgnoreCase);

    // Runs schtasks.exe and returns its exit code, or -1 when it cannot be started or does not finish in time.
    private static int RunSchtasks(params string[] arguments)
    {
        try
        {
            var startInfo = new ProcessStartInfo("schtasks.exe")
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };
            foreach (var argument in arguments)
                startInfo.ArgumentList.Add(argument);

            using var process = Process.Start(startInfo)!;

            // The output is not needed, but an unread pipe could block the process.
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            if (process.WaitForExit(SchtasksTimeout))
                return process.ExitCode;

            process.Kill();
            return -1;
        }
        catch (Exception ex)
        {
            Log.Error("schtasks call failed", ex);
            return -1;
        }
    }
}
