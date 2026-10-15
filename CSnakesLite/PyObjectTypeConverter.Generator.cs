using CSnakesLite.CPython;
using CSnakesLite.Python;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace CSnakesLite;
internal partial class PyObjectTypeConverter
{
    [RequiresDynamicCode(DynamicCodeMessages.CallsMakeGenericType)]
    internal static object ConvertToGeneratorIterator(PyObject pyObject, Type destinationType)
    {
        Debug.Assert(destinationType.IsGenericType);
        Debug.Assert(!destinationType.IsGenericTypeDefinition);
        Debug.Assert(destinationType.GetGenericTypeDefinition() == typeof(IGeneratorIterator<,,>));

        if (!CPythonAPI.IsPyGenerator(pyObject))
        {
            throw new InvalidCastException($"Cannot convert {pyObject.GetPythonType()} to a generator.");
        }

        if (!knownDynamicTypes.TryGetValue(destinationType, out DynamicTypeInfo? typeInfo))
        {
            var typeArgs = destinationType.GetGenericArguments();
            Type generatorType = typeof(GeneratorIterator<,,>).MakeGenericType(typeArgs);
            ConstructorInfo ctor = generatorType.GetConstructors().First();
            typeInfo = new(ctor);
            knownDynamicTypes[destinationType] = typeInfo;
        }

        return typeInfo.ReturnTypeConstructor.Invoke([pyObject.Clone()]);
    }
}
