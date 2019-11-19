using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq; 

namespace HouraiTeahouse {

public static class ArrayUtility {

  /// <summary>
  /// Removes all duplicates from a provided array.
  /// Order, outside of the duplicate values, is retained.
  /// The rest of the array will be filled with the default value of
  /// <typeparamref name="T"/>.
  /// 
  /// Equality is establilshed via the Equals method.
  /// </summary>
  /// <param name="array">the array to remove duplicates of.</param>
  /// <typeparam name="T">the type of the array.</typeparam>
  /// <returns>the number of remaining elements in the </returns>
  public static int RemoveDuplicates<T>(T[] array) {
    int writeIndex = 0;
    for (var i = 0; i < array.Length; i++) {
      if (array[i] == null) continue;
      bool duplicate = false;
      for (var j = 0; j < writeIndex; j++) {
        if (array[i].Equals(array[j])) {
          duplicate = true;
          break;
        }
      }
      if (!duplicate) {
        array[writeIndex++] = array[i];
      }
    }
    for (var i = writeIndex; i < array.Length; i++) {
      array[i] = default(T);
    }
    return writeIndex;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="lhs"></param>
  /// <param name="rhs"></param>
  /// <param name="lhsSize"></param>
  /// <typeparam name="T"></typeparam>
  /// <returns></returns>
  public static int Join<T>(T[] lhs, T[] rhs, int lhsSize) {
    if (lhsSize >= lhs.Length) return lhs.Length;
    int rhsSize = Math.Min(rhs.Length, lhs.Length - lhsSize);
    Array.Copy(rhs, 0, lhs, lhsSize, rhsSize);
    return lhsSize + rhsSize;
  }

  /// <summary>
  /// Converts the provided IEnumerable to an array, preserving order.
  /// 
  /// Unlike Enumerable.ToArray, this uses the shared ArrayPool for
  /// the given type to create the array, minimizing GC. The returned
  /// array is guarenteed to be at least as large as 
  /// <paramref name="value"/>, but could be larger. Use the out param
  /// <paramref name="size"/> to determine how many elements are present. 
  /// The values beyond that value may be any arbitrary value of the type.
  /// </summary>
  /// <param name="values">the enumerable to convert to an array.</param>
  /// <param name="size">the number of elements populated into the array.</param>
  /// <typeparam name="T">the type of array to produce.</typeparam>
  /// <returns>the converted array.</returns>
  public static T[] ConvertToArray<T>(IEnumerable<T> values, out int size) {
    size = values.Count();
    var array = ArrayPool<T>.Shared.Rent(size);
    int index = 0;
    foreach(var val in values) {
      array[index++] = val;
    }
    return array;
  }
 
}

}
