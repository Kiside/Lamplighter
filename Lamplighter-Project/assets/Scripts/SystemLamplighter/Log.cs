using System;
using Godot;
using System.Diagnostics;
using System.Collections.Generic;
using Godot.Collections;

namespace SystemLamplighter
{
	public static class Log
	{
		public static void PrintMessageInCycle(int howMany, string message = "")
		{
			if (howMany <= 0)
				return;
			string callerClass = GetCallerClassName();
			string messageKey = $"[INFO - {callerClass}] {message}";
			if (CycleVariable.PrintInCycle(messageKey, howMany))
			{
				Console.WriteLine($"[INFO - {callerClass}] {message}");
				GD.Print($"[INFO - {callerClass}] {message}");
			}
		}
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

		public static void Dispose()
		{
			CycleVariable.Clear();
		}
	}

	internal static class CycleVariable
	{
		private static System.Collections.Generic.Dictionary<string, int> _stackPrintCycle;

		public static bool PrintInCycle(string message, int howMany)
		{
			if (_stackPrintCycle is null)
				_stackPrintCycle = new System.Collections.Generic.Dictionary<string, int>();

			if (_stackPrintCycle.TryGetValue(message, out int value))
			{
				if (value > 0)
				{
					_stackPrintCycle[message] = value - 1;
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				howMany--;
				if (howMany > 0)
					_stackPrintCycle.Add(message, howMany);
				return true;
			}
		}


		public static void Clear()
		{
			_stackPrintCycle.Clear();
		}


	}
}
