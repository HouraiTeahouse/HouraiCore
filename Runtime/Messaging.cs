using System.Threading.Tasks;
using System;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HouraiTeahouse.FantasyCrescendo {

public static class Messaging {

  /// <summary>
  /// Broadcasts an Action to all child components of a given root.
  /// </summary>
  /// <param name="obj">the root object.</param>
  /// <param name="message">the message to run.</param>
  /// <typeparam name="T">the type to run the Action against.</typeparam>
  /// <returns></returns>
  public static T[] Broadcast<T>(this Object obj, Action<T> message) {
    var components = ObjectUtility.GetAll<T>(obj);
    foreach (var component in components) {
      message(component);
    }
    return components;
  }

  /// <summary>
  /// Broadcasts an asynchronous Action to all child components of a given root.
  /// </summary>
  /// <param name="obj">the root object.</param>
  /// <param name="message">the message to run.</param>
  /// <typeparam name="T">the type to run the Action against.</typeparam>
  /// <returns>the awaitable task.</returns>
  public static Task Broadcast<T>(this Object obj, Func<T, Task> message) =>
    Task.WhenAll(ObjectUtility.GetAll<T>(obj).Select(message));

}

}
