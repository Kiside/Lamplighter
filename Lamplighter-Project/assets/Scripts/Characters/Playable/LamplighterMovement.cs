using Godot;
using System;
using System.Diagnostics;
using SystemLamplighter;
using SystemLamplighter.Common.Input;
using Characters.Abstract;

namespace Characters.Playable
{
	/// <summary>
	/// Classe per il movimento del personaggio in Lamplighter
	/// </summary>
	public partial class LamplighterMovement : AbstractMovement<PlayableCharacterController>
	{
		TurnBasicMovementResolver _turnBasicMovementResolver;

		public override void Init(PlayableCharacterController controller)
		{
			base.Init(controller);
		}

		public override Vector3 Move(double delta)
		{
			Vector3 direction = Vector3.Zero;

			if (Input.IsActionPressed(LamplighterInputMap.Up))
			{
				direction.X += 1;
			}
			if (Input.IsActionPressed(LamplighterInputMap.Down))
			{
				direction.X -= 1;
			}
			if (Input.IsActionPressed(LamplighterInputMap.Left))
			{
				direction.Z -= 1;
			}
			if (Input.IsActionPressed(LamplighterInputMap.Right))
			{
				direction.Z += 1;
			}

			if (direction != Vector3.Zero)
			{
				direction.Normalized();
			}

			_targetVelocity.X = direction.X * Speed;
			_targetVelocity.Z = direction.Z * Speed;


			return _targetVelocity;
		}

		public void BootstrapInit(TurnBasicMovementResolver turnBasicMovementResolver)
		{
			_turnBasicMovementResolver = turnBasicMovementResolver;
		}
	
		public void FreeMove()
		{

		}

		public void CombatMove()
		{

		}
	}
}
