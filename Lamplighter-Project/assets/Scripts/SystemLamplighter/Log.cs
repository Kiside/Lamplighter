using System;
using Godot;
using System.Diagnostics;

namespace SystemLamplighter
{
	public static class Log
	{
		public static void PrintError(string message = "")
		{
			string callerClass = GetCallerClassName();
			Console.WriteLine($"[ERROR - {callerClass}] {message}");
			GD.PrintErr($"[ERROR - {callerClass}] {message}");
		}

		public static void PrintWarning(string message = "")
		{
			string callerClass = GetCallerClassName();
			Console.WriteLine($"[WARNING - {callerClass}] {message}");
			GD.PushWarning($"[WARNING - {callerClass}] {message}");
		}

		public static void PrintMessage(string message = "")
		{
			string callerClass = GetCallerClassName();
			Console.WriteLine($"[INFO - {callerClass}] {message}");
			GD.Print($"[INFO - {callerClass}] {message}");
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
