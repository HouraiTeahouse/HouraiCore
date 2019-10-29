using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Assertions;

namespace HouraiTeahouse {

/// <summary>
/// An unsafe fixed size buffer. 
/// 
/// This only does bounds checking in debug builds.
/// All assetions will be removed in non-debug builds.
/// </summary>
public unsafe readonly ref struct FixedBuffer {

    public readonly byte* Start;
    public readonly byte* End;

    public long Size => End - Start;

    public FixedBuffer(byte* start, byte* end) {
        Assert.IsTrue(start != null && end != null);
        Assert.IsTrue(start <= end);
        Start = start;
        End = end;
    }

    public FixedBuffer(byte* buffer, long size) {
        Assert.IsTrue(buffer != null);
        Start = buffer;
        End = buffer + size;
    }

    public ref byte this[long idx] {
        get {
            Assert.IsTrue(idx >= 0 && idx < Size);
            return ref Start[idx];
        }
    }

    public FixedBuffer Slice(uint end) => Slice(0, end);
    public FixedBuffer Slice(uint start, uint end) {
        Assert.IsTrue(start <= end);
        Assert.IsTrue(Start + end < End);
        return new FixedBuffer(Start + start, Start + end);
    }

    public void CopyTo(FixedBuffer buffer) {
        Assert.IsTrue(buffer.Size >= Size);
        UnsafeUtility.MemCpy(buffer.Start, Start, Size);
    }

    public void CopyTo(void* start, long size) =>
        CopyTo(new FixedBuffer((byte*)start, size));

    public void CopyTo(byte[] buffer, int start = 0) {
        ulong handle;
        var ptr = UnsafeUtility.PinGCArrayAndGetDataAddress(buffer, out handle);
        CopyTo(ptr, buffer.Length);
        UnsafeUtility.ReleaseGCObject(handle);
    }

    public byte[] ToArray() {
        var array = ArrayPool<byte>.Shared.Rent((int)Size);
        CopyTo(array);
        return array;
    }

    public byte[] ToExactArray() {
        var array = new byte[Size];
        CopyTo(array);
        return array;
    }
 
}

}