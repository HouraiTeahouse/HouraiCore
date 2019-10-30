using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HouraiTeahouse {

/// <summary>
/// An abstract class for components that are only supposed to
/// exist in the Editor. Inheriting classes will be destroyed 
/// immedately on instantiation when not in the Editor.
/// </summary>
public abstract class EditorOnlyBehaviour : MonoBehaviour {

    protected virtual void Awake() {
#if !UNITY_EDITOR
        DestroyImmediate(this);
#endif
    }

}

}