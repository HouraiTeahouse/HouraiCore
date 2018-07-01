using System;

namespace HouraiTeahouse {

public static class Argument {

  public static T NotNull<T>(this T obj) {
    if (obj == null) {
      throw new ArgumentNullException();
    }
    return obj;
  }

}

}
