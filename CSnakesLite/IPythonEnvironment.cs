using CSnakesLite.CPython;
using Microsoft.Extensions.Logging;
using System;

namespace CSnakesLite;

public interface IPythonEnvironment : IDisposable
{
    public string Version
    {
        get
        {
            return CPythonAPI.Py_GetVersion() ?? "No version available";
        }
    }

    public string ExecutablePath
    {
        get
        {
            return CPythonAPI.PythonExecutablePath;
        }
    }

    public bool IsDisposed();

    public ILogger? Logger { get; }
}
