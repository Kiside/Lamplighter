using Godot;
using System;

public partial class CharlieController : CharacterBody3D
{

	[Export]
	public NodePath MovementNode;

	private AbstractMovement _movement;

	public override void _Ready()
	{
		base._Ready();

		if (MovementNode != null)
			_movement = GetNode<AbstractMovement>(MovementNode);
		else
			GD.PrintErr("There is no -MovementNode-");
	}
	public override void _PhysicsProcess(double delta)
	{
		Velocity = _movement.Move(delta);
		MoveAndSlide();
	}

}