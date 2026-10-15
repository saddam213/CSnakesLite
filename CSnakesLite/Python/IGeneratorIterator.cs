using System.Collections;
using System.Collections.Generic;

namespace CSnakesLite.Python;

public interface IGeneratorIterator<out TYield, in TSend, out TReturn> :
    IEnumerator<TYield>, IEnumerable<TYield>,
    IGeneratorIterator
{
    /// <remarks>
    /// The value is undefined until either <see cref="IEnumerator.MoveNext"/> or <see cref="Send"/>
    /// has been called and returned <see langword="false"/>.
    /// </remarks>
    TReturn Return { get; }

    bool Send(TSend value);
}

public interface IGeneratorIterator { }
