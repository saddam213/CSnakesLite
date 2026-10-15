using CSnakesLite.Python;
using System;
using System.Diagnostics;

namespace CSnakesLite;

[DebuggerDisplay("Exception Type={PythonExceptionType,nq}, Message={Message,nq}")]
public class PythonInvocationException(string exceptionType,
                                       PyObject? exception,
                                       PyObject? pythonStackTrace,
                                       string customMessage) :
    Exception(customMessage, exceptionType == "StopIteration"
                ? new PythonStopIterationException(exception, pythonStackTrace)
                : new PythonRuntimeException(exception, pythonStackTrace))
{
    public PythonInvocationException(string exceptionType, PyObject? exception, PyObject? pythonStackTrace) :
        this(exceptionType, exception, pythonStackTrace, $"The Python runtime raised a {exceptionType} exception, see InnerException for details.")
    { }

    public string PythonExceptionType { get; } = exceptionType;
}
