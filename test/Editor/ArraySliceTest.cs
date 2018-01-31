using HouraiTeahouse;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Linq;

public class ArraySliceTest {

	[Test]
	public void ArraySlice_is_enumerable_over_entire_array() {
    var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    var arraySlice = new ArraySlice<int>(array);
    Assert.AreEqual(array.Length, arraySlice.Count);
    CollectionAssert.AreEqual(array, arraySlice.ToArray());
	}

	[Test]
	public void ArraySlice_can_be_cut_short() {
    const int kSize = 5;
    var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    var arraySlice = new ArraySlice<int>(array, kSize);
    Assert.AreEqual(kSize, arraySlice.Count);
    Assert.AreNotEqual(array.Length, arraySlice.Count);
    CollectionAssert.AreEqual(array.Take(kSize), arraySlice.ToArray());
	}

	[Test]
	public void ArraySlice_can_started_mid_array() {
    const int kStart = 4;
    const int kSize = 5;
    var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    var arraySlice = new ArraySlice<int>(array, kStart, kStart + kSize);
    Assert.AreEqual(kSize, arraySlice.Count);
    Assert.AreNotEqual(array.Length, arraySlice.Count);
    CollectionAssert.AreEqual(array.Skip(kStart).Take(kSize), arraySlice.ToArray());
	}

	[Test]
	public void ArraySlice_enumerator_fully_enumerates_range() {
    var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    var arraySlice = new ArraySlice<int>(array);
    int index = 0;
    foreach (var val in arraySlice) {
      Assert.AreEqual(array[index], val);
      index++;
    }
    Assert.AreEqual(array.Length, index);
	}

}
