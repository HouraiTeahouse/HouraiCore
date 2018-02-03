using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HouraiTeahouse {

/// <summary>
/// Static utility class for generating permutations of items from multiple generations.
/// </summary>
public static class Permutation {

  /// <summary>
  /// Generates all permutations given a base set of items and an append.
  /// </summary>
  /// <remarks> 
  /// Example 1 (Empty Base Set): ({}, {1, 2, 3}) => {{1}, {2}, {3}}
  /// Example 2 (Empty Next): ({1, 2, 3}, {}) => {}
  /// Example 3 : ({1, 2, 3}, {4, 5, 6}) => {
  ///   {1, 2, 3, 4},
  ///   {1, 2, 3, 5},
  ///   {1, 2, 3, 6},
  /// }
  /// </remarks>
  /// <param name="baseSet">base set of values to work with.</param>
  /// <param name="next">the items to append to the set.</param>
  /// <returns>an enumeration of all permutations of the inputs.</returns>
  public static IEnumerable<object[]> Generate(IEnumerable baseSet, IEnumerable next) {
    var stack = new LinkedList<object>(baseSet.OfType<object>());
    foreach (var obj in next) {
      stack.AddLast(obj);
      yield return stack.ToArray();
      stack.RemoveLast();
    }
  }

  /// <summary>
  /// Generates all permutations, selecting one element from every input enumeration.
  /// </summary>
  /// <param name="enumerations">a list of enumerations to permute.</param>
  /// <returns>All permutations of the set.</returns>
  public static IEnumerable<object[]> GenerateAll(params IEnumerable[] enumerations) {
    List<object[]> lastEnumeration = null;
    foreach (var enumeration in enumerations) {
      if (lastEnumeration == null) {
        var first = enumeration.OfType<object>().Select(obj => new object[] { obj });
        lastEnumeration = new List<object[]>(first);
        continue;
      } else if (lastEnumeration.Count <= 0) {
        return Enumerable.Empty<object[]>();
      }
      var formerSet = lastEnumeration.ToArray();
      lastEnumeration.Clear();
      foreach (var lastEnum in formerSet) {
        lastEnumeration.AddRange(Generate(lastEnum, enumeration));
      }
    }
    return lastEnumeration ?? Enumerable.Empty<object[]>();
  }

}

}