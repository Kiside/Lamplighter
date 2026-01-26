using Godot;
using System;
using SystemLamplighter;

namespace Characters.Playable
{
	public partial class LamplighterCombat : AbstractCombat<PlayableCharacterController>
	{
		ITurnBasedCombat _turnBasedCombat;

		public override void Init(PlayableCharacterController controller)
		{
			base.Init(controller);

			_turnBasedCombat = new TurnBasedCombat();
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
