using System.Collections.Generic;
using Characters.Interfaces;
using Godot;
using SystemLamplighter.DataStructure.GeneralData;
using SystemLamplighter.Debug;

public partial class SelectionTargetRenderer : Node, ITargetRenderer
{

	public override void _Ready()
	{
		base._Ready();
	}

	public void InitMenu(List<ICombatActor> actors)
	{
		
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