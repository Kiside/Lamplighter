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


		/// <summary>
		/// Quando il nodo è pronto viene chiamato il metodo Init
		/// </summary>
		public override void _Ready()
		{
			base._Ready();

			Init();
		}

		/// <summary>
		/// Il metodo Init servirà per inizializzare le variabili importanti per il CharacterController
		/// </summary>
		public virtual void Init() { }

		/// <summary>
		/// Metodo astratto che dovrà venir creato per il controllo dei nodi esposti
		/// </summary>
		protected abstract void NodeChecking();
	}
}
