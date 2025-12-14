using System;
using Godot;
using SystemLamplighter;

namespace Characters
{
	/// <summary>
	/// Classe astratta base per i personaggi
	/// </summary>
	public abstract partial class AbstractCharacterController : CharacterBody3D, ICharacterController
	{

		[Export]
		public NodePath MovementNode;
		[Export]
		public NodePath CombatNode;


		public override void _Ready()
		{
			base._Ready();

			Init();
		}

		public virtual void Init() { }

		protected abstract void NodeChecking();
	}
}
