using CSnakesLite.CPython;
using CSnakesLite.EnvironmentManagement;
using CSnakesLite.Locators;
using CSnakesLite.PackageManagement;
using CSnakesLite.Python;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace CSnakesLite;
public static class PythonEnvironmentHelper
{
    public static IPythonEnvironment CreateEnvironment(string environmentName, string pythonPath, string modulePath, string version, ILogger logger = default)
    {
        var pythonVersion = ServiceCollectionExtensions.ParsePythonVersion(version);
        var environmentPath = Path.Join(modulePath, environmentName);
        var pythonLocator = new FolderLocator(pythonPath, pythonVersion);
        var environmentOptions = new PythonEnvironmentOptions(modulePath, []);
        var environmentManager = new VenvEnvironmentManagement(logger, environmentPath, true);
        return new PythonEnvironment([pythonLocator], [], environmentOptions, logger, environmentManager);
    }

    public static IPythonEnvironment CreateEnvironment(string environmentName, string pythonPath, string modulePath, string requirements, string version, ILogger logger = default)
    {
        var pythonVersion = ServiceCollectionExtensions.ParsePythonVersion(version);
        var environmentPath = Path.Join(modulePath, environmentName);
        var pythonLocator = new FolderLocator(pythonPath, pythonVersion);
        var environmentOptions = new PythonEnvironmentOptions(modulePath, []);
        var environmentManager = new VenvEnvironmentManagement(logger, environmentPath, true);
        var packageInstaller = new PipInstaller(logger, environmentManager, requirements);
        return new PythonEnvironment([pythonLocator], [packageInstaller], environmentOptions, logger, environmentManager);
    }


    public static IEnumerable<T> AsBareEnumerable<T, TImporter>(this PyObject pyObject)
        where TImporter : IPyObjectImporter<T>
    {
        return new PyBareEnumerable<T, TImporter>(pyObject);
    }
}


internal class PyBareEnumerable<TValue, TImporter>(PyObject pyIterable) : IEnumerable<TValue>, IDisposable where TImporter : IPyObjectImporter<TValue>
{
    public void Dispose() => pyIterable.Dispose();

    public IEnumerator<TValue> GetEnumerator() => Iterator(pyIterable.GetIter());

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private static IEnumerator<TValue> Iterator(PyObject pyIterator)
    {
        using var _ = pyIterator;

        while (true)
        {
            TValue import;

            nint result = CPythonAPI.PyIter_Next(pyIterator);
            if (result == IntPtr.Zero)
            {
                if (CPythonAPI.PyErr_Occurred())
                    throw PyObject.ThrowPythonExceptionAsClrException();

                yield break;
            }

            using (var itemObject = PyObject.Create(result))
                import = TImporter.BareImport(itemObject);

            yield return import;
        }
    }
}
