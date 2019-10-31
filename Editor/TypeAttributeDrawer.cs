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

	static Dictionary<Type, TypeInfo> _cache;
	static TypeAttributeDrawer() {
		_cache = new Dictionary<Type, TypeInfo>();
	}

	class TypeInfo {
		public string[] Names;
		public string[] FullNames;
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
		TypeInfo types = Options ?? GetSubtypes(typeAttribute.BaseType);
		var index = Array.IndexOf(Options.FullNames, property.stringValue);
		if (index < 0) {
			index = Mathf.Max(0, Array.IndexOf(Options.FullNames, typeAttribute.Default.FullName));
		}
		index = EditorGUI.Popup(position, label.text, index, Options.Names);
		property.stringValue = types.FullNames[index];
		EditorGUI.EndProperty();
	}

	TypeInfo GetSubtypes(Type baseType) {
		TypeInfo types;
		if (!_cache.TryGetValue(baseType, out types)) {
			types = GetDerivedTypes(baseType);
			_cache[baseType] = types;
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
#if UNITY_2019_2_OR_NEWER
		TypeCache.TypeCollection allTypes = TypeCache.GetTypesDerivedFrom(baseType);
#else
		var allTypes = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
						from type in assembly.GetTypes()
						where !type.IsAbstract && baseType.IsAssignableFrom(type)
						select type);
#endif
		var types = allTypes.Where(t => !t.IsAbstract).ToArray();
		return new TypeInfo {
			Names = types.Select(t => t.Name).ToArray(),
			FullNames = types.Select(t => t.FullName).ToArray()
		};
	}

}

}