using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HouraiTeahouse {

public struct ArraySlice<T> {

  public readonly T[] Array;
  public uint Count => End - Start;
  public uint Start;
  public uint End;

  public ArraySlice(T[] array) : this(array, 0, (uint)array.Length) { }
  public ArraySlice(T[] array, uint end) : this(array, 0, end) { }
  public ArraySlice(T[] array, uint start, uint end) {
    if (start < 0 || start >= end) {
      throw new ArgumentOutOfRangeException(nameof(start), start.ToString());
    }
    if (end < 0 || end > array.Length) {
      throw new ArgumentOutOfRangeException(nameof(end), end.ToString());
    }
    Array = array;
    Start = start;
    End = end;
  }

  public T this[int index] {
    get { return Array[(int)(Start + index)]; }
    set { Array[(int)(Start + index)] = value; }
  }

  public T[] ToArray() {
    var newArray = new T[Count];
    for (var i = 0; i < newArray.Length; i++) {
      newArray[i] = this[i];
    }
    return newArray;
  }

  public Enumerator GetEnumerator() => new Enumerator(this);

  public struct Enumerator {

    public readonly ArraySlice<T> Slice;
    int index;

    public T Current => Slice[index];

    public Enumerator(ArraySlice<T> slice) {
      index = -1;
      Slice = slice;
    }

    public bool MoveNext() {
      index++;
      return index < Slice.Count;
    }

    public void Reset() => index = -1;

  }

}

}