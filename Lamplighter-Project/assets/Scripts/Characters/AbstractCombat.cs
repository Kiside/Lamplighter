using Godot;
using System;

namespace Characters
{
	public abstract partial class AbstractCombat<TController> : Node
	where TController : AbstractCharacterController
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
}

