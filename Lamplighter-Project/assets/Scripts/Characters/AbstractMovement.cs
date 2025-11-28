using Godot;
using System;
using System.Diagnostics;


namespace Characters
{
	public abstract partial class AbstractMovement<TController> : Node
	where TController : AbstractCharacterController
	{
		[Export]
		public int Speed = 14;
		[Export]
		public int Fall_acceleration = 75;

		protected TController _controller;

		protected Vector3 _targetVelocity = Vector3.Zero;

		public virtual void Init(TController controller)
		{
			Debug.Assert(controller != null, "Controller is null");
			_controller = controller;
		}

		public abstract Godot.Vector3 Move(double delta);
	}
}

