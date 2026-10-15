namespace CSnakesLite;
public record PythonEnvironmentOptions(string Home, string[] ExtraPaths, bool InstallSignalHandlers = true, bool CaptureLogs = false);
