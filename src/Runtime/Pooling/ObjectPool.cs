namespace HouraiTeahouse {

public class ObjectPool<T> : AbstractPool<T> where T : new() {

  static ObjectPool<T> sharedIntstance;
  public ObjectPool<T> Shared => sharedIntstance ?? (sharedIntstance = new ObjectPool<T>());

  protected override T CreateNew() => new T();

}

}