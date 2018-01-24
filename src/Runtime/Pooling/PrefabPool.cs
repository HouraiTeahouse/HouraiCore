using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HouraiTeahouse {

public class PrefabPool<T> : AbstractPool<T> where T : UnityEngine.Object {

  readonly T Template;

  public PrefabPool(T template) {
    if (template == null) {
      throw new ArgumentNullException(nameof(template));
    }
    Template = template;
  }

  protected override T CreateNew() => Object.Instantiate(Template);

}

}