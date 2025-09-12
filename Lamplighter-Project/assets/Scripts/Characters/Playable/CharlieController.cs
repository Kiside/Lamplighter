using Godot;
using System;

public partial class CharlieController : AbstractPlayableCharacterController
{

	protected AbstractCombat<CharlieController> _combat;
	protected AbstractMovement<CharlieController> _movement;
	private bool _lockOn = false;

	public bool LockOn { get { return _lockOn; } set { _lockOn = value; } }

	public override void _Ready()
	{
		base._Ready();

		_combat.Init(this);
		_movement.Init(this);
	}

	protected override void NodeChecking()
	{
		string noNode = "There is no ";

		if (MovementNode != null)
			_movement = GetNode<AbstractMovement<CharlieController>>(MovementNode);
		else
			GD.PrintErr($"{noNode} -MovementNode-");

		if (CombatNode != null)
			_combat = GetNode<AbstractCombat<CharlieController>>(CombatNode);
		else
			GD.PrintErr($"{noNode} -CombatNode-");
	}

	public override void _PhysicsProcess(double delta)
	{
		_combat.Combat();
		Velocity = _movement.Move(delta);
		MoveAndSlide();
	}

}