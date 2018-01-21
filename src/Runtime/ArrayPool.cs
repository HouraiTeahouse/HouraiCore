using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HouraiTeahouse.FantasyCrescendo {

/// <summary>
/// Provides a resource pool that enables reusing instances of type <see cref="T:T[]"/>. 
/// </summary>
/// <remarks>
/// <para>
/// Renting and returning buffers with an <see cref="ArrayPool{T}"/> can increase performance
/// in situations where arrays are created and destroyed frequently, resulting in significant
/// memory pressure on the garbage collector.
/// </para>
/// <para>
/// This class is thread-safe.  All members may be used by multiple threads concurrently.
/// </para>
/// </remarks>
public class ArrayPool<T> {

  public readonly int MaxArrayLength;
  public readonly int MaxArraysPerBucket;
  static T[] EmptyArray;

  Bucket[] Buckets;

  /// <summary>The lazily-initialized shared pool instance.</summary>
  private static ArrayPool<T> sharedInstance = null;

  public ArrayPool() : this(20 * 1024 * 1024, 50) {}

  public ArrayPool(int maxArrayLength, int maxArraysPerBucket) {
    if (maxArrayLength <= 0) {
      throw new ArgumentOutOfRangeException(nameof(maxArrayLength));
    }
    if (maxArraysPerBucket <= 0){
      throw new ArgumentOutOfRangeException(nameof(maxArraysPerBucket));
    }
    const int MinimumArrayLength = 0x40, MaximumArrayLength = 0x40000000;
    maxArrayLength = Mathf.Clamp(maxArrayLength, MinimumArrayLength, MaximumArrayLength);
    int maxBuckets = GetBucketIndex(maxArrayLength);
    Buckets = new Bucket[maxBuckets + 1];
    for (var i = 0; i < Buckets.Length; i++) {
      Buckets[i] = new Bucket(16 << i, maxArraysPerBucket);
    }
  }

  /// <summary>
  /// Retrieves a shared <see cref="ArrayPool{T}"/> instance.
  /// </summary>
  /// <remarks>
  /// The shared pool provides a default implementation of <see cref="ArrayPool{T}"/>
  /// that's intended for general applicability.  It maintains arrays of multiple sizes, and 
  /// may hand back a larger array than was actually requested, but will never hand back a smaller 
  /// array than was requested. Renting a buffer from it with <see cref="Rent"/> will result in an 
  /// existing buffer being taken from the pool if an appropriate buffer is available or in a new 
  /// buffer being allocated if one is not available.
  /// </remarks>
  public static ArrayPool<T> Shared => sharedInstance ?? (sharedInstance = new ArrayPool<T>());

  /// <summary>
  /// Retrieves a buffer that is at least the requested length.
  /// </summary>
  /// <param name="minimumLength">The minimum length of the array needed.</param>
  /// <returns>
  /// An <see cref="T:T[]"/> that is at least <paramref name="minimumLength"/> in length.
  /// </returns>
  /// <remarks>
  /// This buffer is loaned to the caller and should be returned to the same pool via 
  /// <see cref="Return"/> so that it may be reused in subsequent usage of <see cref="Rent"/>.  
  /// It is not a fatal error to not return a rented buffer, but failure to do so may lead to 
  /// decreased application performance, as the pool may need to create a new buffer to replace
  /// the one lost.
  /// </remarks>
  public T[] Rent(int minimumLength) {
    if (minimumLength < 0) {
      throw new ArgumentOutOfRangeException(nameof(minimumLength));
    } else if (minimumLength == 0) {
      return EmptyArray ?? (EmptyArray = new T[0]);
    }
    T[] buffer = null;
    int index = GetBucketIndex(minimumLength);
    if (index < Buckets.Length) {
      const int MaxBucketsToTry = 2;
      int i = index;
      do {
        // Attempt to rent from the bucket.  If we get a buffer from it, return it.
        buffer = Buckets[i].Rent();
        if (buffer != null) return buffer;
      } while (++i < Buckets.Length && i != index + MaxBucketsToTry);
    } else {
      buffer = new T[minimumLength];
    }
    return buffer;
  }

  /// <summary>
  /// Returns to the pool an array that was previously obtained via <see cref="Rent"/> on the same 
  /// <see cref="ArrayPool{T}"/> instance.
  /// </summary>
  /// <param name="array">
  /// The buffer previously obtained from <see cref="Rent"/> to return to the pool.
  /// </param>
  /// <param name="clearArray">
  /// If <c>true</c> and if the pool will store the buffer to enable subsequent reuse, <see cref="Return"/>
  /// will clear <paramref name="array"/> of its contents so that a subsequent consumer via <see cref="Rent"/> 
  /// will not see the previous consumer's content.  If <c>false</c> or if the pool will release the buffer,
  /// the array's contents are left unchanged.
  /// </param>
  /// <remarks>
  /// Once a buffer has been returned to the pool, the caller gives up all ownership of the buffer 
  /// and must not use it. The reference returned from a given call to <see cref="Rent"/> must only be
  /// returned via <see cref="Return"/> once.  The default <see cref="ArrayPool{T}"/>
  /// may hold onto the returned buffer in order to rent it again, or it may release the returned buffer
  /// if it's determined that the pool already has enough buffers stored.
  /// </remarks>
  public void Return(T[] array, bool clearArray = false) {
    int index = GetBucketIndex(array.Length);
    if (index < Buckets.Length) {
      if (clearArray) {
        Array.Clear(array, 0, array.Length);
      }
      Buckets[index].Return(array);
    }
  }

  int GetBucketIndex(int bufferSize) {
    uint bitsRemaining = ((uint)bufferSize - 1) >> 4;

    int poolIndex = 0;
    if (bitsRemaining > 0xFFFF) { bitsRemaining >>= 16; poolIndex = 16; }
    if (bitsRemaining > 0xFF)   { bitsRemaining >>= 8;  poolIndex += 8; }
    if (bitsRemaining > 0xF)    { bitsRemaining >>= 4;  poolIndex += 4; }
    if (bitsRemaining > 0x3)    { bitsRemaining >>= 2;  poolIndex += 2; }
    if (bitsRemaining > 0x1)    { bitsRemaining >>= 1;  poolIndex += 1; }

    return poolIndex + (int)bitsRemaining;
  }

  private sealed class Bucket {
    internal readonly int BufferLength;
    readonly T[][] Buffers;

    int Index;

    public Bucket(int bufferLength, int numberOfBuffers) {
      Buffers = new T[numberOfBuffers][];
      BufferLength = bufferLength;
    }

    internal T[] Rent() {
      T[] buffer = null;
      bool allocateBuffer = false;
      if (Index < Buffers.Length) {
        buffer = Buffers[Index];
        Buffers[Index++] = null;
        allocateBuffer = buffer == null;
      }
      if (allocateBuffer) {
        buffer = new T[BufferLength];
      }
      return buffer;
    }

    internal void Return(T[] array) {
      if (array.Length != BufferLength) {
        throw new ArgumentException(nameof(array));
      }
      if (Index != 0) {
        Buffers[--Index] = array;
      }
    }

  }
}

}
