using System;
using UnityEngine;

namespace HouraiTeahouse.Attributes {
		
[AttributeUsage(AttributeTargets.Field)]
public class TypeAttribute :PropertyAttribute {

	public Type BaseType { get; }
	public Type Default { get; set; }
	public string CommonName { get; set; }

	public TypeAttribute(Type baseType) {
		BaseType = baseType;
    Default = baseType;
	}

}

}
