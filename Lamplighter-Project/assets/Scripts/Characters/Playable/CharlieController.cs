using Godot;
using System;
using System.Diagnostics;

namespace Characters.Playable
{
	public partial class CharlieController : AbstractCharacterController, ICharacterControllerAtb
	{

		protected AbstractCombat<CharlieController> _combat;
		protected AbstractMovement<CharlieController> _movement;
		private bool _lockOn = false;

		public bool LockOn { get { return _lockOn; } set { _lockOn = value; } }

		public override void _Ready()
		{
			base._Ready();
			Init();
		}

		public override void Init()
		{
			_combat.Init(this);
			_movement.Init(this);
		}

		protected override void NodeChecking()
		{
			string noNode = "There is no ";
			Debug.Assert(MovementNode != null, $"{noNode} is null.");
			Debug.Assert(CombatNode != null, $"{noNode} is null.");

			if (MovementNode != null)
				_movement = GetNode<AbstractMovement<CharlieController>>(MovementNode);

			if (CombatNode != null)
				_combat = GetNode<AbstractCombat<CharlieController>>(CombatNode);
		}

		public override void _PhysicsProcess(double delta)
		{
			_combat.Combat();
			Velocity = _movement.Move(delta);
			MoveAndSlide();
		}

		public void AtbAction()
		{

		}

	}
}
