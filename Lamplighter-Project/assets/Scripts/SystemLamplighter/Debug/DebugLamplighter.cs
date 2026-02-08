using System.Diagnostics;

namespace SystemLamplighter.Debug
{
	/// <summary>
	/// Classe per il debug di Lamplighter
	/// </summary>
	public static class DebugLamplighter
	{
		public static void Assert(bool condition, string message)
		{
			System.Diagnostics.Debug.Assert(condition, $"[ASSERT {GetCallerClassName()}] - {message}");
		}

		private static string GetCallerClassName()
		{
			var stackTrace = new StackTrace();
			var frame = stackTrace.GetFrame(2);
			var method = frame.GetMethod();
			var type = method.DeclaringType;
			return type != null ? type.Name : "UnknownClass";
		}

	}
}