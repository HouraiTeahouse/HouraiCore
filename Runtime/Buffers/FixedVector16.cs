using UnityEngine;

namespace HouraiTeahouse {

/// <summary>
/// A fixed point 2D vector in the range of [(-1.0, -1.0) - (1.0, 1.0)] in a
/// 2 byte value.
/// </summary>
public struct FixedVector16 {
    public Fixed8 X;
    public Fixed8 Y;

    public static FixedVector16 Zero => new FixedVector16(0f, 0f);
    public static FixedVector16 Up => new FixedVector16(0f, 1f);
    public static FixedVector16 Right => new FixedVector16(1f, 0f);
    public static FixedVector16 One => new FixedVector16(1f, 1f);

    public FixedVector16(Fixed8 x, Fixed8 y) {
        X = x;
        Y = y;
    }

    public FixedVector16(float x, float y) {
        X = (Fixed8)x;
        Y = (Fixed8)y;
    }

    public static explicit operator FixedVector16(Vector2 vec) =>
        new FixedVector16(vec.x, vec.y);
    public static explicit operator Vector2(FixedVector16 vec) =>
        new Vector2((float)vec.X,(float)vec.Y);

    public static FixedVector16 operator +(FixedVector16 val) => val;
    public static FixedVector16 operator -(FixedVector16 val) =>
        new FixedVector16(-val.X, -val.Y);

    public static FixedVector16 operator +(FixedVector16 a, FixedVector16 b) =>
        new FixedVector16(a.X + b.X, a.Y + b.Y);
    public static FixedVector16 operator -(FixedVector16 a, FixedVector16 b) =>
        new FixedVector16(a.X - b.X, a.X - b.Y);

}

}