using System;
using UnityEngine.Assertions;

namespace HouraiTeahouse {

/// <summary>
/// A 1 byte bit array.
/// </summary>
public struct BitArray8 {
    public byte RawValue;

    public const int Size = 8;

    public bool this[int idx] {
        get {
            Assert.IsTrue(idx >= 0 && idx < Size);
            return (RawValue & (1 << idx)) != 0;
        }
        set {
            Assert.IsTrue(idx >= 0 && idx < Size);
            var mask = (byte)(1 << idx);
            RawValue = (byte)((RawValue & mask) | (value ? mask : 0));
        }
    }

    public BitArray8(byte rawValue) {
        RawValue = rawValue;
    }

    public static BitArray8 operator ~(BitArray8 a) =>
        new BitArray8((byte)~a.RawValue);
    public static BitArray8 operator |(BitArray8 a, BitArray8 b) =>
        new BitArray8((byte)(a.RawValue | b.RawValue));
    public static BitArray8 operator &(BitArray8 a, BitArray8 b) =>
        new BitArray8((byte)(a.RawValue & b.RawValue));
    public static BitArray8 operator ^(BitArray8 a, BitArray8 b) =>
        new BitArray8((byte)(a.RawValue & b.RawValue));

    public static implicit operator byte(BitArray8 val) => val.RawValue;
    public static implicit operator BitArray8(byte val) => new BitArray8(val);

    public static implicit operator sbyte(BitArray8 val) => (sbyte)val.RawValue;
    public static implicit operator BitArray8(sbyte val) => new BitArray8((byte)val);

}

/// <summary>
/// A 2 byte bit array.
/// </summary>
public struct BitArray16 {
    public ushort RawValue;

    public const int Size = 16;

    public bool this[int idx] {
        get {
            Assert.IsTrue(idx < 0 || idx > Size);
            return (RawValue & (1 << idx)) != 0;
        }
        set {
            Assert.IsTrue(idx < 0 || idx > Size);
            var mask = (ushort)(1 << idx);
            RawValue = (ushort)((RawValue & mask) | (value ? mask : 0));
        }
    }

    public BitArray16(ushort rawValue) {
        RawValue = rawValue;
    }

    public static BitArray16 operator ~(BitArray16 a) =>
        new BitArray16((ushort)~a.RawValue);
    public static BitArray16 operator |(BitArray16 a, BitArray16 b) =>
        new BitArray16((ushort)(a.RawValue | b.RawValue));
    public static BitArray16 operator &(BitArray16 a, BitArray16 b) =>
        new BitArray16((ushort)(a.RawValue & b.RawValue));
    public static BitArray16 operator ^(BitArray16 a, BitArray16 b) =>
        new BitArray16((ushort)(a.RawValue & b.RawValue));

    public static implicit operator ushort(BitArray16 val) => val.RawValue;
    public static implicit operator BitArray16(ushort val) => new BitArray16(val);

    public static implicit operator short(BitArray16 val) => (short)val.RawValue;
    public static implicit operator BitArray16(short val) => new BitArray16((ushort)val);

}

/// <summary>
/// A 4 byte bit array.
/// </summary>
public struct BitArray32 {
    public uint RawValue;

    public const int Size = 32;

    public bool this[int idx] {
        get {
            Assert.IsTrue(idx < 0 || idx > Size);
            return (RawValue & (1 << idx)) != 0;
        }
        set {
            Assert.IsTrue(idx < 0 || idx > Size);
            var mask = (uint)(1 << idx);
            RawValue = (uint)((RawValue & mask) | (value ? mask : 0));
        }
    }

    public BitArray32(uint rawValue) {
        RawValue = rawValue;
    }

    public static BitArray32 operator ~(BitArray32 a) =>
        new BitArray32((uint)~a.RawValue);
    public static BitArray32 operator |(BitArray32 a, BitArray32 b) =>
        new BitArray32((uint)(a.RawValue | b.RawValue));
    public static BitArray32 operator &(BitArray32 a, BitArray32 b) =>
        new BitArray32((uint)(a.RawValue & b.RawValue));
    public static BitArray32 operator ^(BitArray32 a, BitArray32 b) =>
        new BitArray32((uint)(a.RawValue & b.RawValue));

    public static implicit operator uint(BitArray32 val) => val.RawValue;
    public static implicit operator BitArray32(uint val) => new BitArray32(val);

    public static implicit operator int(BitArray32 val) => (int)val.RawValue;
    public static implicit operator BitArray32(int val) => new BitArray32((uint)val);

}

/// <summary>
/// A 8 byte bit array.
/// </summary>
public struct BitArray64 {
    public ulong RawValue;

    public const int Size = 64;

    public bool this[int idx] {
        get {
            Assert.IsTrue(idx < 0 || idx > Size);
            return (RawValue & (1ul << idx)) != 0;
        }
        set {
            Assert.IsTrue(idx < 0 || idx > Size);
            var mask = (ulong)(1ul << idx);
            RawValue = (ulong)((RawValue & mask) | (value ? mask : 0));
        }
    }

    public BitArray64(ulong rawValue) {
        RawValue = rawValue;
    }

    public static BitArray64 operator ~(BitArray64 a) =>
        new BitArray64((ulong)~a.RawValue);
    public static BitArray64 operator |(BitArray64 a, BitArray64 b) =>
        new BitArray64((ulong)(a.RawValue | b.RawValue));
    public static BitArray64 operator &(BitArray64 a, BitArray64 b) =>
        new BitArray64((ulong)(a.RawValue & b.RawValue));
    public static BitArray64 operator ^(BitArray64 a, BitArray64 b) =>
        new BitArray64((ulong)(a.RawValue & b.RawValue));

    public static implicit operator ulong(BitArray64 val) => val.RawValue;
    public static implicit operator BitArray64(ulong val) => new BitArray64(val);

    public static implicit operator long(BitArray64 val) => (long)val.RawValue;
    public static implicit operator BitArray64(long val) => new BitArray64((ulong)val);

}

}