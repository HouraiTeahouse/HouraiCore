using HouraiTeahouse.EditorAttributes;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace HouraiTeahouse {

/// <summary> 
/// Component that marks a unique object. Objects instantiated with this attached only allows one to exist.
/// Trying to create/instantiate more copies will have the object destroyed instantly. 
/// </summary>
[DisallowMultipleComponent]
public sealed class UniqueObject : MonoBehaviour {

  /// <summary> 
  /// A collection of all of the UniqueObjects currently in the game. 
  /// </summary>
  static Dictionary<int, UniqueObject> AllIDs;

  [SerializeField, ReadOnly, Tooltip("The unique id for this object")]
  int _id;

  [SerializeField]
  bool _dontDestroyOnLoad;

  /// <summary> 
  /// The unique ID of the object. 
  /// </summary>
  public int ID => _id;

  /// <summary>
  /// Awake is called when the script instance is being loaded.
  /// </summary>
  void Awake() {
    if (AllIDs == null) {
      AllIDs = new Dictionary<int, UniqueObject>();
    }
    UniqueObject obj;
    if (AllIDs.TryGetValue(ID, out obj)) {
        // Destroy only destroys the object after a frame is finished, which still allows
        // other code in other attached scripts to execute.
        // DestroyImmediate ensures that said code is not executed and immediately removes the
        // GameObject from the scene.
        Debug.LogWarning($"{obj.name} (ID: {ID}) already exists. Destroying {name}");
        DestroyImmediate(gameObject);
        return;
    }
    AllIDs[ID] = this;
    Debug.Log($"Registered {name} as a unique object. (ID: {ID})");
    if (_dontDestroyOnLoad) {
      DontDestroyOnLoad(gameObject);
    }
  }
  
  /// <summary>
  /// This function is called when the MonoBehaviour will be destroyed.
  /// </summary>
  void OnDestroy() {
    if (AllIDs == null || AllIDs[ID] != this) {
      return;
    }
    AllIDs.Remove(ID);
    if (AllIDs.Count <= 0) {
      AllIDs = null;
    }
  }

  /// <summary>
  /// Reset is called when the user hits the Reset button in the Inspector's
  /// context menu or when adding the component the first time.
  /// </summary>
  void Reset() { 
    _id = new Random().Next(); 
  }

}

}
