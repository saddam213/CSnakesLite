using CSnakesLite.EnvironmentManagement;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace CSnakesLite;

internal partial class PythonEnvironmentBuilder(IServiceCollection services) : IPythonEnvironmentBuilder
{
    private readonly string[] extraPaths = [];
    private string home = Environment.CurrentDirectory;
    private bool installSignalHandlers = true;
    private bool capturePythonLogs = false;

    public IServiceCollection Services { get; } = services;

    public IPythonEnvironmentBuilder WithVirtualEnvironment(string path, bool ensureExists = true)
    {
        Services.AddSingleton<IEnvironmentManagement>(
            sp =>
            {
                var logger = sp.GetService<ILogger<VenvEnvironmentManagement>>();
                return new VenvEnvironmentManagement(logger, path, ensureExists);
            });
        return this;
    }

    public IPythonEnvironmentBuilder WithHome(string home)
    {
        this.home = home;
        return this;
    }

    public PythonEnvironmentOptions GetOptions() => new(home, extraPaths, installSignalHandlers, capturePythonLogs);

    public IPythonEnvironmentBuilder DisableSignalHandlers()
    {
        installSignalHandlers = false;
        return this;
    }

    public IPythonEnvironmentBuilder CapturePythonLogs()
    {
        capturePythonLogs = true;
        return this;
    }
}
