using Godot;
using System;

public abstract partial class AbstractMovement : Node
{
	[Export]
	public int Speed = 14;
	[Export]
	public int Fall_acceleration = 75;

	protected Vector3 _targetVelocity = Vector3.Zero;

	public abstract Godot.Vector3 Move(double delta);
}
