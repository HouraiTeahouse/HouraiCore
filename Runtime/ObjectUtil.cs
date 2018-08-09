using System;
using UnityEngine;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HouraiTeahouse {

/// <summary>
/// A set of generalized helper methods for working with Unity Objects.
/// </summary>
public static class ObjectUtil {

  /// <summary>
  /// Sets the object's enabled/active state. Works with GameObjects and Behaviors.
  /// 
  /// Will enable/disable Behaviours. Will set GameObjects active/inactive.
  /// Does nothing to other objects.
  /// </summary>
  /// <param name="obj">the object to change</param>
  /// <param name="active">the state to set the object to</param>
  public static void SetActive(Object obj, bool active) {
    if (obj == null) return;
    var gameObject = obj as GameObject;
    var behavior = obj as Behaviour;
    if (gameObject != null && gameObject.activeSelf != active ) {
      gameObject.SetActive(active);
    }
    if (behavior != null) {
      behavior.enabled = active;
    }
  }

  /// <summary>
  /// Gets the first component of a type in the sub-hierarchy of the object.
  /// A generalized GetComponentInChildren.
  /// 
  /// Works woth GameObjects and Components.
  /// </summary>
  /// <param name="obj">the root object to search from.</param>
  /// <typeparam name="T">the type of the component to search for.</typeparam>
  /// <returns>the located component, null if <paramref cref="obj"/> is not an GameObject or component or if none is found</returns>
  public static T GetFirst<T>(Object obj) where T : Component {
    if (obj == null) return null;
    var gameObject = obj as GameObject;
    var component = obj as Component;
    if (component != null) {
      gameObject = component.gameObject;
    }
    if (gameObject != null) {
      return gameObject.GetComponentInChildren<T>();
    }
    return null;
  }

  /// <summary>
  /// Gets all components of a type in the sub-hierarchy of the object.
  /// A generalized GetComponentsInChildren.
  /// 
  /// Works woth GameObjects and Components.
  /// </summary>
  /// <param name="obj">the root object to search from.</param>
  /// <typeparam name="T">the type of the component to search for.</typeparam>
  /// <returns>the located components, empty if <paramref cref="obj"/> is not an GameObject or component or if none is found</returns>
  public static T[] GetAll<T>(Object obj) where T : Component {
    if (obj == null) return new T[0];
    var gameObject = obj as GameObject;
    var component = obj as Component;
    if (component != null) {
      gameObject = component.gameObject;
    }
    if (gameObject != null) {
      return gameObject.GetComponentsInChildren<T>();
    }
    return new T[0];
  }

  /// <summary>
  /// Gets all components of a type in the sub-hierarchy of the object.
  /// A generalized GetComponentsInChildren.
  /// 
  /// Works woth GameObjects and Components.
  /// </summary>
  /// <param name="obj">the root object to search from.</param>
  /// <param name="type">the type of the component to search for.</param>
  /// <returns>the located components, empty if <paramref cref="obj"/> is not an GameObject or component or if none is found</returns>
  public static Component[] GetAll(Object obj, Type type) {
    if (obj == null) return new Component[0];
    var gameObject = obj as GameObject;
    var component = obj as Component;
    if (component != null) {
      gameObject = component.gameObject;
    }
    if (gameObject != null) {
      return gameObject.GetComponentsInChildren(type);
    }
    return new Component[0];
  }

  /// <summary>
  /// Destroys all components of a type in the sub-hierarchy of the object.
  /// 
  /// Works woth GameObjects and Components.
  /// Uses Object.DestroyImmediate if used in the Editor while the game is not playing.
  /// Uses Object.Destroy otherwise.
  /// </summary>
  /// <param name="obj">the root object to search from.</param>
  /// <param name="type">the type of the component to search for.</param>
  public static void DestroyAll<T>(Object obj) where T : Component {
    foreach (var comp in GetAll<T>(obj)) {
      Destroy(comp);
    }
  }

  /// <summary>
  /// Destroys all components of a type in the sub-hierarchy of the object.
  /// 
  /// Works woth GameObjects and Components.
  /// Uses Object.DestroyImmediate if used in the Editor while the game is not playing.
  /// Uses Object.Destroy otherwise.
  /// </summary>
  /// <param name="obj">the root object to search from.</param>
  /// <param name="type">the type of the component to search for.</param>
  public static void DestroyAll(Object obj, Type type) {
    foreach (var comp in GetAll(obj, type)) {
      Destroy(comp);
    }
  }

  /// <summary>
  /// Destroys the provided object.
  /// 
  /// Uses Object.DestroyImmediate if used in the Editor while the game is not playing.
  /// This will destroy assets.
  /// 
  /// Uses Object.Destroy otherwise.
  /// </summary>
  /// <param name="obj">the object to destroy</param>
  public static void Destroy(Object obj) {
    if (obj == null) return;
#if UNITY_EDITOR
    if (!EditorApplication.isPlayingOrWillChangePlaymode) {
      Object.DestroyImmediate(obj, true);
    } else
#endif
    {
      Object.Destroy(obj);
    }
  }

  /// <summary>
  /// Forces a Unity references to true null instead of Unity's faux-null.
  /// </summary>
  /// <param name="obj">the object to force to null if it is a faux-null</param>
  /// <typeparam name="T">the type of the object to work with</typeparam>
  /// <returns>the forced null reference</returns>
  public static T ForceNull<T>(this T obj) where T : Object {
    return obj == null ? null : obj;
  }

}

}