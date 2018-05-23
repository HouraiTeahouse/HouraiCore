using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HouraiTeahouse {

public abstract class AbstractPool<T> {

  public const int DefaultMaxCapacity = 1000;

  public readonly int MaxCapacity;
  readonly Queue<T> Pool;

  protected AbstractPool() : this(DefaultMaxCapacity) {}

  protected AbstractPool(int MaxCapacity) {
    Pool = new Queue<T>();
  }

  public virtual T Rent() {
    if (Pool.Count <= 0) {
      return CreateNew();
    } else {
      return Pool.Dequeue();
    }
  }

  public virtual bool Return(T obj) {
    if (Pool.Count + 1 > MaxCapacity) return false;
    Pool.Enqueue(obj);
    return true;
  }

  public void Prewarm(int count) {
    count = Mathf.Min(count, MaxCapacity - Pool.Count);
    if (count <= 0) return;
    for (var i = 0; i < count; i++) {
      Pool.Enqueue(CreateNew());
    }
  }

  protected abstract T CreateNew();

}

}
