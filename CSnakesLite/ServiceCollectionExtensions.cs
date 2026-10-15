using CSnakesLite.EnvironmentManagement;
using CSnakesLite.Locators;
using CSnakesLite.PackageManagement;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Text.RegularExpressions;

namespace CSnakesLite;
/// <summary>
/// Extension methods for <see cref="IServiceCollection"/> to configure Python-related services.
/// </summary>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Python-related services to the service collection with the specified Python home directory.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <param name="home">The Python home directory.</param>
    /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
    public static IPythonEnvironmentBuilder WithPython(this IServiceCollection services)
    {
        var pythonBuilder = new PythonEnvironmentBuilder(services);

        services.AddSingleton<IPythonEnvironmentBuilder>(pythonBuilder);
        services.AddSingleton<IPythonEnvironment>(sp =>
        {
            var envBuilder = sp.GetRequiredService<IPythonEnvironmentBuilder>();
            var locators = sp.GetServices<PythonLocator>();
            var installers = sp.GetServices<IPythonPackageInstaller>();
            var logger = sp.GetService<ILogger<IPythonEnvironment>>();
            var environmentManager = sp.GetService<IEnvironmentManagement>();

            var options = envBuilder.GetOptions();

            return new PythonEnvironment(locators, installers, options, logger, environmentManager);
        });
        return pythonBuilder;
    }

    public static Version ParsePythonVersion(string version)
    {
        // Remove non -numeric characters except .
        Match versionMatch = VersionParseExpr().Match(version);
        if (!versionMatch.Success)
        {
            throw new InvalidOperationException($"Invalid Python version: '{version}'");
        }

        if (!Version.TryParse(versionMatch.Value, out Version? parsed))
        {
            throw new InvalidOperationException($"Failed to parse Python version: '{version}'");
        }

        if (parsed.Build == -1)
        {
            return new Version(parsed.Major, parsed.Minor, 0, 0);
        }

        if (parsed.Revision == -1)
        {
            return new Version(parsed.Major, parsed.Minor, parsed.Build, 0);
        }

        return parsed;
    }

    /// <summary>
    /// Adds a Python locator using Python from a specific folder to the service collection with the specified folder path and version.
    /// </summary>
    /// <param name="builder">The <see cref="IPythonEnvironmentBuilder"/> to add the locator to.</param>
    /// <param name="folder">The path to the folder.</param>
    /// <param name="version">The version of the Python installation.</param>
    /// <returns>The modified <see cref="IPythonEnvironmentBuilder"/>.</returns>
    public static IPythonEnvironmentBuilder FromFolder(this IPythonEnvironmentBuilder builder, string folder, string version)
    {
        builder.Services.AddSingleton<PythonLocator>(new FolderLocator(folder, ParsePythonVersion(version)));
        return builder;
    }

    /// <summary>
    /// Adds a pip package installer to the service collection.
    /// </summary>
    /// <param name="builder">The <see cref="IPythonEnvironmentBuilder"/> to add the installer to.</param>
    /// <param name="requirementsPath">The path to the requirements file.</param>
    /// <returns>The modified <see cref="IPythonEnvironmentBuilder"/>.</returns>
    public static IPythonEnvironmentBuilder WithPipInstaller(this IPythonEnvironmentBuilder builder, string requirementsPath = "requirements.txt")
    {
        builder.Services.AddSingleton<IPythonPackageInstaller>(
            sp =>
            {
                var logger = sp.GetService<ILogger<PipInstaller>>();
                var environmentManager = sp.GetService<IEnvironmentManagement>();
                return new PipInstaller(logger, environmentManager, requirementsPath);
            }
        );
        return builder;
    }

    [GeneratedRegex("^(\\d+(\\.\\d+)*)")]
    private static partial Regex VersionParseExpr();
}
