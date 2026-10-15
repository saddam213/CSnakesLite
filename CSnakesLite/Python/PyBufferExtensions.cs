using System;
using System.Numerics.Tensors;

namespace CSnakesLite.Python;
public static class PyBufferExtensions
{
    #region AsSpan
    public static Span<bool> AsBoolSpan(this IPyBuffer buffer) => buffer.AsSpan<bool>();
    public static Span<byte> AsByteSpan(this IPyBuffer buffer) => buffer.AsSpan<byte>();
    public static Span<sbyte> AsSByteSpan(this IPyBuffer buffer) => buffer.AsSpan<sbyte>();

    public static Span<short> AsInt16Span(this IPyBuffer buffer) => buffer.AsSpan<short>();
    public static Span<ushort> AsUInt16Span(this IPyBuffer buffer) => buffer.AsSpan<ushort>();

    public static Span<int> AsInt32Span(this IPyBuffer buffer) => buffer.AsSpan<int>();

    public static Span<uint> AsUInt32Span(this IPyBuffer buffer) => buffer.AsSpan<uint>();

    public static Span<long> AsInt64Span(this IPyBuffer buffer) => buffer.AsSpan<long>();

    public static Span<ulong> AsUInt64Span(this IPyBuffer buffer) => buffer.AsSpan<ulong>();

    public static Span<Half> AsHalfSpan(this IPyBuffer buffer) => buffer.AsSpan<Half>();
    public static Span<float> AsFloatSpan(this IPyBuffer buffer) => buffer.AsSpan<float>();

    public static Span<double> AsDoubleSpan(this IPyBuffer buffer) => buffer.AsSpan<double>();
    public static Span<nint> AsIntPtrSpan(this IPyBuffer buffer) => buffer.AsSpan<nint>();
    public static Span<nuint> AsUIntPtrSpan(this IPyBuffer buffer) => buffer.AsSpan<nuint>();
    #endregion

    #region AsReadOnlySpan
    public static ReadOnlySpan<bool> AsBoolReadOnlySpan(this IPyBuffer buffer) => buffer.AsReadOnlySpan<bool>();
    public static ReadOnlySpan<byte> AsByteReadOnlySpan(this IPyBuffer buffer) => buffer.AsReadOnlySpan<byte>();
    public static ReadOnlySpan<sbyte> AsSByteReadOnlySpan(this IPyBuffer buffer) => buffer.AsReadOnlySpan<sbyte>();
    public static ReadOnlySpan<short> AsInt16ReadOnlySpan(this IPyBuffer buffer) => buffer.AsReadOnlySpan<short>();
    public static ReadOnlySpan<ushort> AsUInt16ReadOnlySpan(this IPyBuffer buffer) => buffer.AsReadOnlySpan<ushort>();
    public static ReadOnlySpan<int> AsInt32ReadOnlySpan(this IPyBuffer buffer) => buffer.AsReadOnlySpan<int>();
    public static ReadOnlySpan<uint> AsUInt32ReadOnlySpan(this IPyBuffer buffer) => buffer.AsReadOnlySpan<uint>();
    public static ReadOnlySpan<long> AsInt64ReadOnlySpan(this IPyBuffer buffer) => buffer.AsReadOnlySpan<long>();
    public static ReadOnlySpan<ulong> AsUInt64ReadOnlySpan(this IPyBuffer buffer) => buffer.AsReadOnlySpan<ulong>();
    public static ReadOnlySpan<Half> AsHalfReadOnlySpan(this IPyBuffer buffer) => buffer.AsReadOnlySpan<Half>();
    public static ReadOnlySpan<float> AsFloatReadOnlySpan(this IPyBuffer buffer) => buffer.AsReadOnlySpan<float>();
    public static ReadOnlySpan<double> AsDoubleReadOnlySpan(this IPyBuffer buffer) => buffer.AsReadOnlySpan<double>();
    public static ReadOnlySpan<nint> AsIntPtrReadOnlySpan(this IPyBuffer buffer) => buffer.AsReadOnlySpan<nint>();
    public static ReadOnlySpan<nuint> AsUIntPtrReadOnlySpan(this IPyBuffer buffer) => buffer.AsReadOnlySpan<nuint>();
    #endregion

    #region AsTensorSpan

    public static TensorSpan<bool> AsBoolTensorSpan(this IPyBuffer buffer) => buffer.AsTensorSpan<bool>();
    public static TensorSpan<byte> AsByteTensorSpan(this IPyBuffer buffer) => buffer.AsTensorSpan<byte>();
    public static TensorSpan<sbyte> AsSByteTensorSpan(this IPyBuffer buffer) => buffer.AsTensorSpan<sbyte>();
    public static TensorSpan<short> AsInt16TensorSpan(this IPyBuffer buffer) => buffer.AsTensorSpan<short>();
    public static TensorSpan<ushort> AsUInt16TensorSpan(this IPyBuffer buffer) => buffer.AsTensorSpan<ushort>();
    public static TensorSpan<int> AsInt32TensorSpan(this IPyBuffer buffer) => buffer.AsTensorSpan<int>();
    public static TensorSpan<uint> AsUInt32TensorSpan(this IPyBuffer buffer) => buffer.AsTensorSpan<uint>();
    public static TensorSpan<long> AsInt64TensorSpan(this IPyBuffer buffer) => buffer.AsTensorSpan<long>();
    public static TensorSpan<ulong> AsUInt64TensorSpan(this IPyBuffer buffer) => buffer.AsTensorSpan<ulong>();
    public static TensorSpan<Half> AsHalfTensorSpan(this IPyBuffer buffer) => buffer.AsTensorSpan<Half>();
    public static TensorSpan<float> AsFloatTensorSpan(this IPyBuffer buffer) => buffer.AsTensorSpan<float>();
    public static TensorSpan<double> AsDoubleTensorSpan(this IPyBuffer buffer) => buffer.AsTensorSpan<double>();
    public static TensorSpan<nint> AsIntPtrTensorSpan(this IPyBuffer buffer) => buffer.AsTensorSpan<nint>();
    public static TensorSpan<nuint> AsUIntPtrTensorSpan(this IPyBuffer buffer) => buffer.AsTensorSpan<nuint>();

    public static ReadOnlyTensorSpan<bool> AsBoolReadOnlyTensorSpan(this IPyBuffer buffer) => buffer.AsReadOnlyTensorSpan<bool>();
    public static ReadOnlyTensorSpan<byte> AsByteReadOnlyTensorSpan(this IPyBuffer buffer) => buffer.AsReadOnlyTensorSpan<byte>();
    public static ReadOnlyTensorSpan<sbyte> AsSByteReadOnlyTensorSpan(this IPyBuffer buffer) => buffer.AsReadOnlyTensorSpan<sbyte>();
    public static ReadOnlyTensorSpan<short> AsInt16ReadOnlyTensorSpan(this IPyBuffer buffer) => buffer.AsReadOnlyTensorSpan<short>();
    public static ReadOnlyTensorSpan<ushort> AsUInt16ReadOnlyTensorSpan(this IPyBuffer buffer) => buffer.AsReadOnlyTensorSpan<ushort>();
    public static ReadOnlyTensorSpan<int> AsInt32ReadOnlyTensorSpan(this IPyBuffer buffer) => buffer.AsReadOnlyTensorSpan<int>();
    public static ReadOnlyTensorSpan<uint> AsUInt32ReadOnlyTensorSpan(this IPyBuffer buffer) => buffer.AsReadOnlyTensorSpan<uint>();
    public static ReadOnlyTensorSpan<long> AsInt64ReadOnlyTensorSpan(this IPyBuffer buffer) => buffer.AsReadOnlyTensorSpan<long>();
    public static ReadOnlyTensorSpan<ulong> AsUInt64ReadOnlyTensorSpan(this IPyBuffer buffer) => buffer.AsReadOnlyTensorSpan<ulong>();
    public static ReadOnlyTensorSpan<Half> AsHalfReadOnlyTensorSpan(this IPyBuffer buffer) => buffer.AsReadOnlyTensorSpan<Half>();
    public static ReadOnlyTensorSpan<float> AsFloatReadOnlyTensorSpan(this IPyBuffer buffer) => buffer.AsReadOnlyTensorSpan<float>();
    public static ReadOnlyTensorSpan<double> AsDoubleReadOnlyTensorSpan(this IPyBuffer buffer) => buffer.AsReadOnlyTensorSpan<double>();
    public static ReadOnlyTensorSpan<nint> AsIntPtrReadOnlyTensorSpan(this IPyBuffer buffer) => buffer.AsReadOnlyTensorSpan<nint>();
    public static ReadOnlyTensorSpan<nuint> AsUIntPtrReadOnlyTensorSpan(this IPyBuffer buffer) => buffer.AsReadOnlyTensorSpan<nuint>();

    #endregion
}
