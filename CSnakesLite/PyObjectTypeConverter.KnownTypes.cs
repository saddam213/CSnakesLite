using CSnakesLite.Python;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CSnakesLite;
internal partial class PyObjectTypeConverter
{
    static readonly Type listType = typeof(IReadOnlyList<>);
    static readonly Type collectionType = typeof(IEnumerable);
    static readonly Type dictionaryType = typeof(IReadOnlyDictionary<,>);
    static readonly Type pyObjectType = typeof(PyObject);
    static readonly Type generatorIteratorType = typeof(IGeneratorIterator<,,>);
}
