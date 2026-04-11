using Godot;
using System;
using System.Diagnostics;
using SystemLamplighter;
using SystemLamplighter.Common.Input;
using Characters.Abstract;
using SystemLamplighter.Bootstrap;
using SystemLamplighter.Debug;

namespace Characters.Playable
{
	/// <summary>
	/// Classe per il movimento del personaggio in Lamplighter
	/// </summary>
	public partial class LamplighterMovement : AbstractMovement<PlayableCharacterController>
	{
		TurnBasicMovementResolver _turnBasicMovementResolver;

		// TODO: creare una classe che si occupa del calcolo che viene fatto ora in RealtimeMove

		public void BootstrapInit(TurnBasicMovementResolver turnBasicMovementResolver)
		{
			_turnBasicMovementResolver = turnBasicMovementResolver;
		}

		public override void Init(PlayableCharacterController controller)
		{
			base.Init(controller);

			DebugLamplighter.Assert(_turnBasicMovementResolver != null, "_turnBasicMovementResolver is null");
		}

		public override Vector3 RealtimeMove(double delta)
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

		public override Godot.Vector3[] PointToPointMove(Godot.Vector3 from, Godot.Vector3 to) => _turnBasicMovementResolver.ResolveMovement(from, to);
	
		public void FreeMove()
		{

		}

		public void CombatMove()
		{

		}
	}
}
