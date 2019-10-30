using System;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HouraiTeahouse {

/// <summary> 
/// Takes screenshots upon pressing a specified keyboard key
/// </summary>
public class Screenshot : MonoBehaviour {

  public Key Key = Key.F12;
  public string DateTimeFormat = "MM-dd-yyyy-HHmmss";
  public string Format = "screenshot-{0}";

  /// <summary>
  /// Update is called every frame, if the MonoBehaviour is enabled.
  /// </summary>
  void Update() {
    if (!Keyboard.current[Key].wasPressedThisFrame) return;
    string filename = string.Format(Format, DateTime.UtcNow.ToString(DateTimeFormat)) + ".png";
    string path = Path.Combine(Application.dataPath, filename);
    Debug.LogFormat($"Screenshot taken. Saved to {path}");
    ScreenCapture.CaptureScreenshot(Application.platform == RuntimePlatform.IPhonePlayer ? filename : path);
  }

}

}