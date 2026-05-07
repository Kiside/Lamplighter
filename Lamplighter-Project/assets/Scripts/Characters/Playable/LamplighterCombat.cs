using Godot;
using System;
using SystemLamplighter;
using SystemLamplighter.Events;
using SystemLamplighter.Extensions;
using Characters.Abstract;
using SystemLamplighter.Interfaces;
using SystemLamplighter.Combat.Core;
using SystemLamplighter.Tool;
using SystemLamplighter.DataStructure.GeneralData;
using System.Diagnostics;
using SystemLamplighter.Debug;
using System.Collections.Generic;
using Characters.Interfaces;

namespace Characters.Playable;
/// <summary>
/// Classer per il combattimento in Lamplighter
/// </summary>
public partial class LamplighterCombat : AbstractCombat<LamplighterCharacterController>
{
	ITurnBasedCombat _turnBasedCombat;
	LamplighterCharacterController _controller;

	IMovementService _movementService;

	public ICombatActor CombatActor => _controller.CombatActor;

	[Export]
	NodePath AnimationPlayerPath;

	AnimationPlayer _animationPlayer;

	public override void _EnterTree()
	{
		if(AnimationPlayerPath != null)
			_animationPlayer = GetNode<AnimationPlayer>(AnimationPlayerPath);
		base._EnterTree();
	}

	public override void Init(LamplighterCharacterController controller)
	{
		_controller = controller;
		base.Init(controller);
	}

	public void BootstrapInit(ITurnBasedCombat turnBasedCombat, IMovementService movementService)
	{
		DebugLamplighter.Assert(turnBasedCombat != null, "turnBasedCombat is null");
		
		_turnBasedCombat = turnBasedCombat;

		_turnBasedCombat.Init(new TurnBasedCombatContext(_controller.BattleMenuController,
				_controller.CombatActor,
				this.GetPublisher<AtbCommandPhaseEndEvent>(),
				this.GetPublisher<AtbEndExecuteActionEvent>(),
				this.GetPublisher<StartTargetEvent>(),
				this.GetSubscriber<AtbCommandPhaseStartedEvent>(),
				this.GetSubscriber<AtbExecuteActionEvent>(),
				this.GetSubscriber<EndTargetEvent>()),
				movementService,
				_animationPlayer);
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


