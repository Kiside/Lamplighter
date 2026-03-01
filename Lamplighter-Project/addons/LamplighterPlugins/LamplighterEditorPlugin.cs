#if TOOLS
using Godot;
using System;

[Tool]
public partial class LamplighterEditorPlugin : EditorPlugin
{
	public override void _EnterTree()
	{
		var script = GD.Load<Script>("res://assets/Scripts/SystemLamplighter/Debug/DebugButton.cs");
		var texture = GD.Load<Texture2D>("res://assets/icon.svg");
		AddCustomType("DebugButton", "Button", script, texture);
	}

	public override void _ExitTree()
	{
		// Clean-up of the plugin goes here.
		RemoveCustomType("DebugButton");
		RemoveCustomType("SelectionLabel");
	}
}
#endif
