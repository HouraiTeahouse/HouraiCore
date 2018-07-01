#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

namespace HouraiTeahouse {

public static class EditorAssetUtil {

  const string ResourcePath = "Resources/";
  static readonly Regex ResourceRegex = new Regex(".*/Resources/(.*?)\\..*", RegexOptions.Compiled);

  /// <summary>
  /// Loads all assets of a type within the project.
  /// </summary>
  /// <returns>an enumeration of objects loaded.</returns>
  public static IEnumerable<T> LoadAll<T>() => LoadAll(typeof(T)).OfType<T>();

  /// <summary>
  /// Loads all assets of a type within the project.
  /// </summary>
  /// <param name="type">the type of object to find</param>
  /// <returns>an enumeration of objects loaded.</returns>
  public static IEnumerable<Object> LoadAll(Type type) {
    var paths = from guid in AssetDatabase.FindAssets($"t:{type.Name}")
                select AssetDatabase.GUIDToAssetPath(guid);
    return from path in paths select AssetDatabase.LoadAssetAtPath<Object>(path);
  }

  /// <summary>
  /// Finds and loads the asset associated with a asset bundle and asset name.
  /// </summary>
  /// <param name="bundleName">the bundle to look in.</param>
  /// <param name="assetName">the bundle to look for.</param>
  /// <returns>the loaded asset.</returns>
  public static T LoadBundledAsset<T>(string bundleName, string assetName) where T : Object {
    string[] path = AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName(bundleName, assetName);
    return AssetDatabase.LoadAssetAtPath<T>(path.FirstOrDefault());
  }

  /// <summary>
  /// Gets whether an asset is addressable to a location loadable via Resources.Load.
  /// </summary>
  /// <param name="asset">the asset to evaluate.</param>
  /// <returns>true if the asset can be loaded from Resources.Load, false otherwise.</returns>
  public static bool IsResource(Object asset) {
    return IsResourcePath(AssetDatabase.GetAssetPath(asset));
  }

  /// <summary>
  /// Get's the valid path to pass into Resources.Load for a given asset object.
  /// </summary>
  /// <param name="asset"></param>
  /// <returns></returns>
  public static string GetResourcePath(Object asset) {
    string assetPath = AssetDatabase.GetAssetPath(asset);
    if (!IsResourcePath(assetPath)) {
      return string.Empty;
    }
    return ResourceRegex.Replace(assetPath, "$1");
  }

  static bool IsResourcePath(string path) {
    return !string.IsNullOrEmpty(path) && path.Contains(ResourcePath);
  }

}

}
#endif