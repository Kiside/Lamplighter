using Godot;
using System;
using SystemLamplighter;
using SystemLamplighter.Events;
using SystemLamplighter.Extensions;
using Characters.Abstract;
using SystemLamplighter.Interfaces;
using SystemLamplighter.Combat.Core;

namespace Characters.Playable;
/// <summary>
/// Classer per il combattimento in Lamplighter
/// </summary>
	public partial class LamplighterCombat : AbstractCombat<PlayableCharacterController>
	{
		ITurnBasedCombat _turnBasedCombat;

		public override void Init(PlayableCharacterController controller)
		{
			base.Init(controller);

			_turnBasedCombat = new TurnBasedCombat(controller.BattleMenuController, 
			controller.CombatActor,
			this.GetPublisher<AtbCommandPhaseEndEvent>(),
			this.GetPublisher<AtbEndExecuteActionEvent>(),
			this.GetSubscriber<AtbCommandPhaseStartedEvent>(),
			this.GetSubscriber<AtbExecuteActionEvent>()
			 );
		}

		public override void Combat()
		{

		}

		protected void LockOn()
		{

		}

		protected void Attack()
		{

		}

		public override void _ExitTree()
		{
			_turnBasedCombat.Dispose();
			base._ExitTree();
		}
	}
