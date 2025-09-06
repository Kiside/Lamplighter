using Godot;
using System;

public abstract partial class AbstractMovement : Node
{
	[Export]
	public int speed = 14;
	[Export]
	public int fall_acceleration = 75;

	public abstract Godot.Vector3 Move(double delta);
}
