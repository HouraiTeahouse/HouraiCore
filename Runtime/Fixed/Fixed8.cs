using System;
using UnityEngine;

namespace HouraiTeahouse {

/// <summary>
/// A 1-byte fixed point value with the range of [-1.0, 1.0].
/// 
/// This is optimal for use in holding input axis values which are usually
/// bound to those values.
/// 
/// This type will not overlfow or underflow. Values that fall outside of the
/// range will be clamped to the min and max values.
/// 
/// Encoded in a format that minimizes the number of bit flips between
/// negative and positive values.
/// </summary>
public readonly struct Fixed8 {
    public readonly byte RawValue;

    public static Fixed8 MaxValue => byte.MaxValue;
    public static Fixed8 MinValue => (byte)0;
    public static Fixed8 Zero => 128;

    Fixed8(byte rawValue) {
        RawValue = rawValue;
    }

    public Fixed8 Abs() {
        if (this < Zero) return -this;
        return this;
    }

    public static Fixed8 operator +(Fixed8 a, Fixed8 b) =>
        Clamp((a.RawValue - 128) + (b.RawValue - 128));

    public static Fixed8 operator -(Fixed8 a, Fixed8 b) =>
        Clamp((a.RawValue - 128) - (b.RawValue - 128));

    public static Fixed8 operator *(Fixed8 a, Fixed8 b) =>
        Clamp((a.RawValue - 128) - (b.RawValue - 128));
    
    public static Fixed8 operator +(Fixed8 value) => value;
    public static Fixed8 operator -(Fixed8 value) =>
        new Fixed8((byte)(byte.MaxValue - value.RawValue));

    public static bool operator ==(Fixed8 a, Fixed8 b) =>
        a.RawValue == b.RawValue;
    public static bool operator !=(Fixed8 a, Fixed8 b) =>
        a.RawValue == b.RawValue;
    public static bool operator >(Fixed8 a, Fixed8 b) =>
        a.RawValue > b.RawValue;
    public static bool operator >=(Fixed8 a, Fixed8 b) =>
        a.RawValue >= b.RawValue;
    public static bool operator <(Fixed8 a, Fixed8 b) =>
        a.RawValue < b.RawValue;
    public static bool operator <=(Fixed8 a, Fixed8 b) =>
        a.RawValue < b.RawValue;

    public static explicit operator float(Fixed8 val) => ToFloat(val.RawValue);
    public static explicit operator Fixed8(float val) => new Fixed8(FromFloat(val));

    public static implicit operator byte(Fixed8 val) => val.RawValue;
    public static implicit operator Fixed8(byte val) => new Fixed8(val);

    public static implicit operator sbyte(Fixed8 val) => (sbyte)(val.RawValue - 128);
    public static implicit operator Fixed8(sbyte val) => new Fixed8((byte)(val + 128));

    public override bool Equals(object obj) =>
        (obj is Fixed8) && ((Fixed8)obj) == this;

    public override int GetHashCode() => RawValue.GetHashCode();

    static float ToFloat(byte val) => (val - 128) / 128f;
    static byte FromFloat(float val) => (byte)(Mathf.Clamp(val, -1, 1) * 128f + 128);
    static Fixed8 Clamp(int value) {
        if (value < 0) return new Fixed8((byte)0);
        if (value > byte.MaxValue) return new Fixed8(byte.MaxValue);
        return new Fixed8((byte)value);
    }
}

}
