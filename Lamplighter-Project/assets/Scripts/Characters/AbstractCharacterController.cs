using System;
using Godot;

namespace Characters
{
	public abstract partial class AbstractCharacterController : CharacterBody3D, ICharacterController
	{

		[Export]
		public NodePath MovementNode;
		[Export]
		public NodePath CombatNode;


		public override void _Ready()
		{
			base._Ready();

			NodeChecking();
		}

		public virtual void Init() { }

		protected abstract void NodeChecking();
	}
}
