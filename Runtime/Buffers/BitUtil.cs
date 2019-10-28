using System.Runtime.CompilerServices;

namespace HouraiTeahouse {

/// <summary>
/// Static class for manipulating individual bits in a value.
/// </summary>
public static class BitUtil {

    /// <summary>
    /// Gets whether a bit is set or not.
    /// </summary>
    /// <param name="value">the value to read from</param>
    /// <param name="bit">the bit to read from.</param>
    /// <returns>true if set, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool GetBit(byte value, int bit) => (value & (1 << bit)) != 0;

    /// <summary>
    /// Sets a bit to a value.
    /// </summary>
    /// <param name="value">a reference to the value that needs to be set.</param>
    /// <param name="bit">which bit to set</param>
    /// <param name="bitValue">the value to set the bit to.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetBit(ref byte value, int bit, bool bitValue) {
        var mask = 1 << bit;
        value = (byte)(bitValue ? (value | mask) : (value & ~mask));
    }

    /// <summary>
    /// Creates a mask for all bit slower than max.
    /// </summary>
    /// <param name="max">the highest bit.</param>
    /// <returns>the created mask.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte AllBits(int max) => (byte)((1 << max) - 1);

    /// <summary>
    /// Counts how many bits have been set in a value.
    /// </summary>
    /// <param name="val">the value to count the bits of</param>
    /// <returns>the count of bits set.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetBitCount(byte val) {
        int x = val;
        x = (x & 0x55) + (x >> 1 & 0x55);
        x = (x & 0x33) + (x >> 2 & 0x33);
        x = (x & 0x0f) + (x >> 4 & 0x0f);
        return x;
    }

}

}