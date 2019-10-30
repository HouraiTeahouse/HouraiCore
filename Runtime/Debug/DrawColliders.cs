using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HouraiTeahouse {
    
/// <summary>
/// Debug script for persistently drawing the all of the colliders under it.
/// </summary>
public class DrawColliders : EditorOnlyBehaviour {

  public Color GizmoColor;
  public bool Wire;

#if UNITY_EDITOR
  /// <summary>
  /// Callback to draw gizmos that are pickable and always drawn.
  /// </summary>
  void OnDrawGizmos() {
    using (GizmoUtility.With(GizmoColor)) {
      foreach (var collider in ObjectUtility.GetAll<Collider>(this)) {
        GizmoUtility.DrawCollider(collider, Wire);
      }
    }
  }
#endif 

}

}