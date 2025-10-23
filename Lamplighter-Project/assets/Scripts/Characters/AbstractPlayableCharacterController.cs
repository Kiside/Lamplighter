using System;
using Godot;

namespace Characters
{
	public abstract partial class AbstractPlayableCharacterController : CharacterBody3D
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

		protected abstract void NodeChecking();
	}
}
