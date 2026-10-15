using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CSnakesLite;

/// <summary>
/// Represents a builder for creating Python environments.
/// </summary>
public interface IPythonEnvironmentBuilder
{
    public IServiceCollection Services { get; }

    /// <summary>
    /// Sets the virtual environment path for the Python environment being built.
    /// </summary>
    /// <param name="path">The path to the virtual environment.</param>
    /// <param name="ensureEnvironment">Indicates whether to ensure the virtual environment exists.</param>
    /// <returns>The current instance of the <see cref="IPythonEnvironmentBuilder"/>.</returns>
    IPythonEnvironmentBuilder WithVirtualEnvironment(string path, bool ensureEnvironment = true);

    /// <summary>
    /// Sets the home directory for the Python environment being built.
    /// </summary>
    /// <param name="home">The home directory path.</param>
    /// <returns>The current instance of the <see cref="IPythonEnvironmentBuilder"/>.</returns>
    IPythonEnvironmentBuilder WithHome(string home);

    /// <summary>
    /// Disables signal handlers for the Python environment being built.
    /// </summary>
    /// <returns>The current instance of the <see cref="IPythonEnvironmentBuilder"/>.</returns>
    IPythonEnvironmentBuilder DisableSignalHandlers();

    /// <summary>
    /// Capture Python logs and emit them to the <see cref="ILogger"/> API used in the environment
    /// </summary>
    /// <returns>The current instance of the <see cref="IPythonEnvironmentBuilder"/>.</returns>
    IPythonEnvironmentBuilder CapturePythonLogs();

    /// <summary>
    /// Gets the options for the Python environment being built.
    /// </summary>
    /// <returns>The <see cref="PythonEnvironmentOptions"/> for the current environment.</returns>
    PythonEnvironmentOptions GetOptions();
}
