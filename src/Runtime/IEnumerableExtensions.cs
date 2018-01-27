using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HouraiTeahouse {

public static class IEnumerableExtensions {

  class ReadOnlyCollection<T> : IReadOnlyCollection<T> {

    readonly IEnumerable<T> Collection;

    public ReadOnlyCollection(IEnumerable<T> collection) {
      Collection = collection;
    }

    public int Count => Collection.Count();

    public IEnumerator<T> GetEnumerator() => Collection.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => Collection.GetEnumerator();

  }

  public static IReadOnlyCollection<T> AsReadOnly<T>(this IEnumerable<T> collection) {
    return new ReadOnlyCollection<T>(Argument.NotNull(collection));
  }

}

}