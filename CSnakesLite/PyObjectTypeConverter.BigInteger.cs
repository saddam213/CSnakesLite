using CSnakesLite.CPython;
using CSnakesLite.Python;
using System;
using System.Numerics;

namespace CSnakesLite;
internal partial class PyObjectTypeConverter
{
    internal static BigInteger ConvertToBigInteger(PyObject pyObject, Type destinationType) =>
        // There is no practical API for this in CPython. Use str() instead. 
        BigInteger.Parse(pyObject.ToString());

    internal static PyObject ConvertFromBigInteger(BigInteger integer)
    {
        using PyObject pyUnicode = PyObject.Create(CPythonAPI.AsPyUnicodeObject(integer.ToString()));
        return PyObject.Create(CPythonAPI.PyLong_FromUnicodeObject(pyUnicode, 10));
    }
}
