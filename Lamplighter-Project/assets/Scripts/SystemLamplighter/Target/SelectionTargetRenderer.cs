using Godot;
using SystemLamplighter.DataStructure.GeneralData;
using SystemLamplighter.Debug;

public partial class SelectionTargetRenderer : Node, ITargetRenderer
{
	[Export]
	PackedScene _selectionRowScene;

	public override void _Ready()
	{
		DebugLamplighter.Assert(_selectionRowScene != null, "selectionRowScene is empty");

		base._Ready();

		//if(_selectionRowScene)
	}

	public bool CanRender(TargetCursorState targetCursorState)
	{
		if(targetCursorState is ActorCursorState)
			return true;
		
		return false;
	}

	public void Render(TargetCursorState targetCursorState)
	{
		
	}
}