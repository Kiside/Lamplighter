using Godot;
using System;

public abstract partial class AbstractCombat<TController> : Node
where TController : AbstractPlayableCharacterController
{
	[Export]
	public float Damage = 0;

	TController _controller;

	public virtual void Init(TController controller)
	{
		_controller = controller;
	}

	public abstract void Combat();
}
