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
using System.IO;

namespace Characters.Playable
{
	/// <summary>
	/// Classe per il movimento del personaggio in Lamplighter
	/// </summary>
	public partial class LamplighterMovement : AbstractMovement<LamplighterCharacterController>
	{
		[Export]
		NodePath _navAgentPath;

		NavigationAgent3D _navAgent;
		TurnBasedMovementGlobalResolver _turnBasicMovementResolver;

		MovementService _movementService;

		public Queue<Godot.Vector3> Movement {get; private set;}

		// TODO: creare una classe che si occupa del calcolo che viene fatto ora in RealtimeMove

		public void BootstrapInit(IMovementService movementService)
		{
			DebugLamplighter.Assert(movementService != null, "movementService is null");
			
			_movementService = movementService as MovementService;
			_movementService.SetNavigationAgent(_navAgent);
		}

		public override void Init(LamplighterCharacterController controller)
		{
			base.Init(controller);

			DebugLamplighter.Assert(_navAgentPath != null, "_navAgentPath is null");

			if(_navAgentPath != null)
				_navAgent = GetNode<NavigationAgent3D>(_navAgentPath);

			DebugLamplighter.Assert(_navAgent != null, "_navAgent is null");
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
			if (_movementService == null)
				return null;			
			return _movementService.TargetPositionMovement(_controller.GlobalPosition) * Speed;
		}

		public override bool IsMovementFinished()
		{
			return _movementService.IsNavigationFinished();
		}

		public void FreeMove()
		{

		}

		public void CombatMove()
		{

		}
	}
}
