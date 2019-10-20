using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace HouraiTeahouse.Attributes {

[CustomPropertyDrawer(typeof(TypeAttribute))]
public class TypeAttributeDrawer : PropertyDrawer {

	static Dictionary<Type, TypeInfo> TypeCache;

	class TypeInfo {
		public string[] Names;
		public string[] FullNames;
	}

	static TypeAttributeDrawer() {
		TypeCache = new Dictionary<Type, TypeInfo>();
	}

	TypeInfo Options;
	TypeAttribute typeAttribute => attribute as TypeAttribute;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		if (property.propertyType != SerializedPropertyType.String) {
			Debug.LogWarning($"Should not be using a TypeAttribute on a non-string field: {property.propertyPath}");
			EditorGUI.PropertyField(position, property, label, true);
			return;
		}

		EditorGUI.BeginProperty(position, label, property);
		var targetAttr = attribute as TypeAttribute;
		TypeInfo types = Options ?? GetSubtypes(targetAttr.BaseType);
		var index = Array.IndexOf(Options.FullNames, property.stringValue);
		if (index < 0) {
			index = Mathf.Max(0, Array.IndexOf(Options.FullNames, targetAttr.Default.FullName));
		}
		index = EditorGUI.Popup(position, label.text, index, Options.Names);
		property.stringValue = types.FullNames[index];
		EditorGUI.EndProperty();
	}

	TypeInfo GetSubtypes(Type baseType) {
		TypeInfo types;
		if (!TypeCache.TryGetValue(baseType, out types)) {
			types = GetDerivedTypes(baseType);
			TypeCache[baseType] = types;
		}
		if (typeAttribute.CommonName != null) {
			types.Names = types.Names.Select(n => {
				if (n == typeAttribute.CommonName) return n;
				return n.Replace(typeAttribute.CommonName, "");
			}).ToArray();
		}
		Options = types;
		return types;
	}

	static TypeInfo GetDerivedTypes(Type baseType) {
		var types = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
								 from type in assembly.GetTypes()
								 where !type.IsAbstract && baseType.IsAssignableFrom(type)
								 select type).ToArray();
		return new TypeInfo {
			Names = types.Select(t => t.Name).ToArray(),
			FullNames = types.Select(t => t.FullName).ToArray()
		};
	}

}

}