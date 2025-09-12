using Godot;
using System;

public abstract partial class AbstractMovement<TController> : Node
where TController : AbstractPlayableCharacterController
{
	[Export]
	public int Speed = 14;
	[Export]
	public int Fall_acceleration = 75;

	protected TController _controller;

	protected Vector3 _targetVelocity = Vector3.Zero;

	public virtual void Init(TController controller)
	{
		_controller = controller;
	}

	public abstract Godot.Vector3 Move(double delta);
}
