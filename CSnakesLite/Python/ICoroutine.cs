using System.Threading;
using System.Threading.Tasks;

namespace CSnakesLite.Python;

public interface ICoroutine<TYield, TSend, TReturn> : ICoroutine
{
    public Task<TYield> AsTask(CancellationToken cancellationToken = default);
}

public interface ICoroutine { }
