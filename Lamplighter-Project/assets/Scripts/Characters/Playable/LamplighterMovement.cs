using Godot;
using System;
using System.Diagnostics;
using SystemLamplighter;
using SystemLamplighter.Common.Input;
using Characters.Abstract;
using SystemLamplighter.Bootstrap;
using SystemLamplighter.Debug;
using System.Collections.Generic;
using SystemLamplighter.Tool;

namespace Characters.Playable
{
	/// <summary>
	/// Classe per il movimento del personaggio in Lamplighter
	/// </summary>
	public partial class LamplighterMovement : AbstractMovement<PlayableCharacterController>
	{
		TurnBasedMovementGlobalResolver _turnBasicMovementResolver;

		public Queue<Godot.Vector3> Movement {get; private set;}

		// TODO: creare una classe che si occupa del calcolo che viene fatto ora in RealtimeMove

		public void BootstrapInit(TurnBasedMovementGlobalResolver turnBasicMovementResolver)
		{
			DebugLamplighter.Assert(turnBasicMovementResolver != null, "turnBasicMovementResolver is null");
			
			_turnBasicMovementResolver = turnBasicMovementResolver;
		}

		public override void Init(PlayableCharacterController controller)
		{
			base.Init(controller);
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

		public override Godot.Vector3? PointToPointMove(Godot.Vector3 from, Godot.Vector3 to, Identification id)
		{			
			if(_turnBasicMovementResolver.GetMovement(id).Count > 0)
				return _turnBasicMovementResolver.GetMovement(id).Dequeue();
			else 
				return null;

			// if(_turnBasicMovementResolver.Movement.Count != 0)
			// 	Log.PrintMessage($"----MOVIMENTO DIVERSO DA ZERO-----{_turnBasicMovementResolver.Movement}");
			// if (_turnBasicMovementResolver.Movement.Count == 0)
			// 	return null;

			// return _turnBasicMovementResolver.Movement.Dequeue();
		}

		public void FreeMove()
		{

		}

		public void CombatMove()
		{

		}
	}
}
