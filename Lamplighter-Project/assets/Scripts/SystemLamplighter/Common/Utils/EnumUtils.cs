using System;
using System.Collections.Generic;
using System.Linq;

namespace SystemLamplighter.Common.Utils;

public static class EnumUtils
{
	public static List<string> FromEnumtoList<T>() => Enum.GetNames(typeof(T)).ToList();

}