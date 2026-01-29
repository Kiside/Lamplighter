using Godot;
using System;
using SystemLamplighter;
using SystemLamplighter.Events;
using SystemLamplighter.Extensions;

namespace Characters.Playable
{
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
	}
}
