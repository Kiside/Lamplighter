#if TOOLS
using Godot;
using System;

[Tool]
public partial class LamplighterPlugins : EditorPlugin
{
	public override void _EnterTree()
	{
		var script = GD.Load<Script>("res://assets/Scripts/SystemLamplighter/DebugButton.cs");
		var texture = GD.Load<Texture2D>("res://assets/icon.svg");
		AddCustomType("DebugButton", "Button", script, texture);
		// Initialization of the plugin goes here.
	}

	public override void _ExitTree()
	{
		// Clean-up of the plugin goes here.
		RemoveCustomType("DebugButton");
	}
}
#endif
