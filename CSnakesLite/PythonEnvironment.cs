using CSnakesLite.CPython;
using CSnakesLite.EnvironmentManagement;
using CSnakesLite.Locators;
using CSnakesLite.PackageManagement;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSnakesLite;

internal class PythonEnvironment : IPythonEnvironment
{
    public ILogger? Logger { get; private set; }

    private readonly CPythonAPI api;
    private bool disposedValue;
    private IAsyncDisposable? pythonCaptureLogger;

    internal PythonEnvironment(
        IEnumerable<PythonLocator> locators,
        IEnumerable<IPythonPackageInstaller> packageInstallers,
        PythonEnvironmentOptions options,
        ILogger? logger,
        IEnvironmentManagement? environmentManager = null)
    {
        Logger = logger;

        var location = locators
            .Where(locator => locator.IsSupported())
            .Select(locator => locator.LocatePython())
            .FirstOrDefault(loc => loc is not null);

        if (location is null)
        {
            logger?.LogError("Python installation not found. There were {LocatorCount} locators registered.", locators.Count());
            throw new InvalidOperationException("Python installation not found.");
        }

        string home = options.Home;
        string[] extraPaths = options.ExtraPaths;

        home = Path.GetFullPath(home);
        if (!Directory.Exists(home))
        {
            logger?.LogError("Python home directory does not exist: {Home}", home);
            throw new DirectoryNotFoundException("Python home directory does not exist.");
        }

        if (environmentManager is not null)
        {

            extraPaths = [.. options.ExtraPaths, environmentManager.GetExtraPackagePath(location!)];

            environmentManager.EnsureEnvironment(location);
        }

        logger?.LogDebug("Setting up Python environment from {PythonLocation} using home of {Home}", location.Folder, home);

        foreach (var installer in packageInstallers)
        {
            installer.InstallPackagesFromRequirements(home);
        }

        char sep = Path.PathSeparator;

        api = SetupCPythonAPI(location, options);

        if (!string.IsNullOrEmpty(home))
        {
            api.PythonPath = api.PythonPath + sep + home;
        }

        if (extraPaths is { Length: > 0 })
        {
            logger?.LogDebug("Adding extra paths to PYTHONPATH: {ExtraPaths}", extraPaths);
            api.PythonPath = api.PythonPath + sep + string.Join(sep, extraPaths);
        }
        api.Initialize();

        if (options.CaptureLogs)
        {
            if (logger is null)
                throw new ArgumentNullException(nameof(logger), "Argument cannot be null when capturing Python logs.");
            pythonCaptureLogger = PythonLogger.EnableGlobalLogging(logger);
        }
    }

    private CPythonAPI SetupCPythonAPI(PythonLocationMetadata pythonLocationMetadata, PythonEnvironmentOptions options)
    {
        string pythonDll = pythonLocationMetadata.LibPythonPath;
        string pythonPath = pythonLocationMetadata.PythonPath;

        Logger?.LogDebug("Python DLL: {PythonDLL}", pythonDll);
        Logger?.LogDebug("Python path: {PythonPath}", pythonPath);

        var api = new CPythonAPI(pythonDll, pythonLocationMetadata.Version, pythonLocationMetadata.PythonBinaryPath, options.InstallSignalHandlers)
        {
            PythonPath = pythonPath
        };
        return api;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                pythonCaptureLogger?.DisposeAsync().GetAwaiter().GetResult();
                api.Dispose();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public bool IsDisposed()
    {
        return disposedValue;
    }
}
