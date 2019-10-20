using UnityEditor;
using UnityEngine;

namespace HouraiTeahouse.Attributes {

/// <summary>
/// Custom PropertyDrawer for ReadOnlyAttribute.
/// </summary>
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
internal class ReadOnlyAttributeDrawer : PropertyDrawer {

  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
    GUI.enabled = false;
    EditorGUI.PropertyField(position, property);
    GUI.enabled = true;
  }

}

}
