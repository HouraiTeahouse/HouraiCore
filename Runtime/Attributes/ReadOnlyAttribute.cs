using System;
using UnityEngine;

namespace HouraiTeahouse.Attributes {

/// <summary>
/// A PropertyAttribute to mark serializable fields as read-only.
/// They will appear in the editor as a simple label or disabled control.
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class ReadOnlyAttribute : PropertyAttribute {
}

}
