using Godot;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.Interfaces;
using SystemLamplighter.Setup;
using SystemLamplighter.Tool;

namespace SystemLamplighter.Visual;

// TODO: Da cancellare?

public partial class Highlightable : Node,  IHighlightable
{
	/// <summary>
	/// Material for the highlight
	/// </summary>
	[Export]
	private Material _material;

	public override void _EnterTree()
	{
		base._EnterTree();
	}

	public void ApplyHighlight()
	{
		Log.PrintMessage("HIGHGLIGHT!");
	}

	public void RemoveHighlight()
	{
		Log.PrintMessage("REMOVED HIGHLIGHT!");
	}
} 