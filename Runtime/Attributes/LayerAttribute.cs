using System;
using UnityEngine;

namespace HouraiTeahouse.Attributes {

/// <summary>
/// A PropertyAttribute that exposes a Layer control on the editor
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class LayerAttribute : PropertyAttribute {
}

}
