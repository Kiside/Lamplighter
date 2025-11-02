using Godot;
using System;
using SystemLamplighter;

public partial class DebugAtb : Node
{
	public NodePath Atb;
	private ActionTimeBattleController _atbController;

	public override void _Ready()
	{
		base._Ready();

		_atbController = GetNode<ActionTimeBattleController>(Atb);
	}

	public void StartCombatClick()
	{
		_atbController.ContinueAtb();
	}

	public void StopCombatClick()
	{

	}

	public void RemoveCharacter()
	{

	}

	public void AddCharacter()
	{

	}
}
