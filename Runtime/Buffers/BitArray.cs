using System;

namespace HouraiTeahouse {

/// <summary>
/// A 1 byte bit array.
/// </summary>
public struct BitArray8 {
    public byte RawValue;

    public const int Size = 8;

    public bool this[int idx] {
        get {
            if (idx < 0 || idx > Size) throw new IndexOutOfRangeException(idx.ToString());
            return (RawValue & (1 << idx)) != 0;
        }
        set {
            if (idx < 0 || idx > Size) throw new IndexOutOfRangeException(idx.ToString());
            var mask = (byte)(1 << idx);
            RawValue = (byte)((RawValue & mask) | (value ? mask : 0));
        }
    }

    public BitArray8(byte rawValue) {
        RawValue = rawValue;
    }

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
            if (idx < 0 || idx > Size) throw new IndexOutOfRangeException(idx.ToString());
            return (RawValue & (1 << idx)) != 0;
        }
        set {
            if (idx < 0 || idx > Size) throw new IndexOutOfRangeException(idx.ToString());
            var mask = (ushort)(1 << idx);
            RawValue = (ushort)((RawValue & mask) | (value ? mask : 0));
        }
    }

    public BitArray16(ushort rawValue) {
        RawValue = rawValue;
    }

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
            if (idx < 0 || idx > Size) throw new IndexOutOfRangeException(idx.ToString());
            return (RawValue & (1 << idx)) != 0;
        }
        set {
            if (idx < 0 || idx > Size) throw new IndexOutOfRangeException(idx.ToString());
            var mask = (uint)(1 << idx);
            RawValue = (uint)((RawValue & mask) | (value ? mask : 0));
        }
    }

    public BitArray32(uint rawValue) {
        RawValue = rawValue;
    }

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
            if (idx < 0 || idx > Size) throw new IndexOutOfRangeException(idx.ToString());
            return (RawValue & (1ul << idx)) != 0;
        }
        set {
            if (idx < 0 || idx > Size) throw new IndexOutOfRangeException(idx.ToString());
            var mask = (ulong)(1ul << idx);
            RawValue = (ulong)((RawValue & mask) | (value ? mask : 0));
        }
    }

    public BitArray64(ulong rawValue) {
        RawValue = rawValue;
    }

    public static implicit operator ulong(BitArray64 val) => val.RawValue;
    public static implicit operator BitArray64(ulong val) => new BitArray64(val);

    public static implicit operator long(BitArray64 val) => (long)val.RawValue;
    public static implicit operator BitArray64(long val) => new BitArray64((ulong)val);

}

}