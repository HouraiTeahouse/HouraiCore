using System;

namespace HouraiTeahouse {

public class ObjectPool<T> : AbstractPool<T> where T : new() {

  static ObjectPool<T> sharedIntstance;
  public static ObjectPool<T> Shared => sharedIntstance ?? (sharedIntstance = new ObjectPool<T>());

  protected override T CreateNew() => new T();

  public RentedDisposable Borrow() => new RentedDisposable(this);

  public struct RentedDisposable : IDisposable { 

    public readonly T RentedObject;
    public readonly ObjectPool<T> Pool;

    public RentedDisposable(ObjectPool<T> pool) {
      Pool = pool;
      RentedObject = pool.Rent();
    }

    public void Dispose() => Pool.Return(RentedObject);

  }

}

}