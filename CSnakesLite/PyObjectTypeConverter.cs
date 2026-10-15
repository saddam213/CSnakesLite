using CSnakesLite.CPython;
using CSnakesLite.Python;
using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace CSnakesLite;
internal partial class PyObjectTypeConverter
{
    private static readonly ConcurrentDictionary<Type, DynamicTypeInfo> knownDynamicTypes = [];

    [RequiresDynamicCode(DynamicCodeMessages.CallsMakeGenericType)]
    public static object PyObjectToManagedType(PyObject pyObject,
                                               [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)]
                                               Type destinationType)
    {
        if (CPythonAPI.IsPyDict(pyObject) && IsAssignableToGenericType(destinationType, dictionaryType))
        {
            return ConvertToDictionary(pyObject, destinationType);
        }

        if (CPythonAPI.IsPyList(pyObject) && IsAssignableToGenericType(destinationType, listType))
        {
            return ConvertToList(pyObject, destinationType);
        }

        // This needs to come after lists, because sequences are also maps
        if (CPythonAPI.IsPyMappingWithItems(pyObject) && destinationType.IsAssignableTo(collectionType))
        {
            return ConvertToDictionary(pyObject, destinationType, useMappingProtocol: true);
        }

        if (CPythonAPI.IsPySequence(pyObject) && IsAssignableToGenericType(destinationType, listType))
        {
            return ConvertToList(pyObject, destinationType);
        }

        throw new InvalidCastException($"Attempting to cast {destinationType} from {pyObject.GetPythonType()}");
    }

    record DynamicTypeInfo(ConstructorInfo ReturnTypeConstructor, ConstructorInfo? TransientTypeConstructor = null);
}
