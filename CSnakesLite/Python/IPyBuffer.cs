using System;
using System.Numerics.Tensors;

namespace CSnakesLite.Python;
public interface IPyBuffer : IDisposable
{
    /// <summary>
    /// The bit length of the buffer.
    /// </summary>
    long Length { get; }

    /// <summary>
    /// The number of dimensions in the buffer. For a scalar, this is 1.
    /// </summary>
    int Dimensions { get; }

    /// <summary>
    /// Indicates if the buffer is a scalar (single dimension array).
    /// </summary>
    bool IsScalar { get; }

    ReadOnlySpan<nint> Shape { get; }

    /// <summary>
    /// Gets the item type of the values in the buffer. 
    /// </summary>
    /// <returns></returns>
    Type GetItemType();

    Span<T> AsSpan<T>() where T : unmanaged;
    ReadOnlySpan<T> AsReadOnlySpan<T>() where T : unmanaged;

    TensorSpan<T> AsTensorSpan<T>() where T : unmanaged;
    ReadOnlyTensorSpan<T> AsReadOnlyTensorSpan<T>() where T : unmanaged;
}
