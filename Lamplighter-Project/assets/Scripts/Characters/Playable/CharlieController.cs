using Godot;
using System;

public partial class CharlieController : CharacterBody3D
{
	[Export]
	public AbstractMovement movement;

	public override void _PhysicsProcess(double delta)
	{
		movement.Move(delta);
	}

}