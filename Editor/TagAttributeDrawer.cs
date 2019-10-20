using UnityEngine;
using UnityEditor;

namespace HouraiTeahouse.Attributes {

/// <summary>
/// Custom PropertyDrawer for TagAttribute
/// </summary>
[CustomPropertyDrawer(typeof(TagAttribute))]
public class TagAttributeDrawer : PropertyDrawer {

  /// <summary>
  /// <see cref="PropertyDrawer.OnGUI" />
  /// </summary>
  public override void OnGUI(Rect position,
                             SerializedProperty property,
                             GUIContent label) {
    if (property.propertyType != SerializedPropertyType.String) {
      Debug.LogWarning("A Tag Attribute is being applied to a non-string field.");
      EditorGUI.PropertyField(position, property, label);
      return;
    }
    EditorGUI.BeginProperty(position, label, property);
    property.stringValue = EditorGUI.TagField(position, label,
                                              property.stringValue);
    EditorGUI.EndProperty();
  }

}

}
