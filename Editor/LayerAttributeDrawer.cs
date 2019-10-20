using UnityEngine;
using UnityEditor;

namespace HouraiTeahouse.Attributes {

/// <summary>
/// Custom PropertyDrawer for LayerAttribute
/// </summary>
[CustomPropertyDrawer(typeof(LayerAttribute))]
internal class LayerAttributeDrawer : PropertyDrawer {

  public override void OnGUI(Rect position,
                             SerializedProperty property,
                             GUIContent label) {
    if (property.propertyType != SerializedPropertyType.Integer) {
      base.OnGUI(position, property, label);
      return;
    }

    EditorGUI.BeginProperty(position, label, property);
    property.intValue = EditorGUI.LayerField(position, label, property.intValue);
    EditorGUI.EndProperty();
  }

}

}
