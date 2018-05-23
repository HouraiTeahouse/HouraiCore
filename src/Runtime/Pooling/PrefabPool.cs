using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HouraiTeahouse {

public class PooledEffect : MonoBehaviour {

  public PrefabPool Pool;

  ParticleSystem[] particleSystems;
  AudioSource[] audioSources;

  /// <summary>
  /// Awake is called when the script instance is being loaded.
  /// </summary>
  void Awake() {
    particleSystems = GetComponentsInChildren<ParticleSystem>();
    audioSources = GetComponentsInChildren<AudioSource>();
    if (particleSystems.Length + audioSources.Length <= 0) {
      Destroy(this);
    }
  }

  /// <summary>
  /// Update is called every frame, if the MonoBehaviour is enabled.
  /// </summary>
  void Update() {
    bool isPlaying = false;
    foreach (var particle in particleSystems) {
      isPlaying |= particle != null && particle.isPlaying;
    }
    foreach (var audio in audioSources) {
      isPlaying |= audio != null && audio.isPlaying;
    }
    if (!isPlaying) {
      Pool.Return(gameObject);
    }
  }

}

public class PrefabPool : PrefabPool<GameObject> {

  static Dictionary<GameObject, PrefabPool> prefabs_; 

  static PrefabPool() {
    prefabs_ = new Dictionary<GameObject, PrefabPool>();
  }

  private PrefabPool(GameObject prefab) : base(prefab) {}

  public static PrefabPool Get(GameObject prefab) {
    PrefabPool pool;
    if (prefabs_.TryGetValue(prefab, out pool)) {
      return pool;
    }
    pool = new PrefabPool(prefab);
    prefabs_[prefab] = pool;
    return pool;
  }

  public override GameObject Rent() {
    var obj = base.Rent();
    obj.SetActive(true);
    GetOrAddComponent(obj).Pool = this;
    return obj;
  }

  public override bool Return(GameObject obj) {
    var result = base.Return(obj);
    if (result) {
      ObjectUtil.SetActive(obj, false);
    } else {
      Object.Destroy(obj);
    }
    return result;
  }

  PooledEffect GetOrAddComponent(GameObject gameObj) {
    PooledEffect obj = gameObj.GetComponent<PooledEffect>();
    if (obj == null) {
      obj = gameObj.AddComponent<PooledEffect>();
    }
    return obj;
  }

}

public class PrefabPool<T> : AbstractPool<T> where T : UnityEngine.Object {

  readonly Func<T> createFun;

  public PrefabPool(T template) {
    if (template == null) {
      throw new ArgumentNullException(nameof(template));
    }
    createFun = () => Object.Instantiate(template);
  }

  public PrefabPool(Func<T> createFunc) {
    if (createFunc == null) {
      throw new ArgumentNullException(nameof(createFunc));
    }
    createFun = createFunc;
  }

  protected override T CreateNew() => createFun();

  public override T Rent() {
    // Because pool elements can be destroyed by external changes (i.e scene changes),
    // the base results may return destroyed (null) elements. Keep pulling until it 
    // we find one that is valid.
    T val;
    do {
      val = base.Rent();
    } while (val == null);
    return val;
  }

}

}