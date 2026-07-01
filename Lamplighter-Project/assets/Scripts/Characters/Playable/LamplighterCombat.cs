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
	#region Export variable
	[Export]
	NodePath AnimationPlayerPath;
	#endregion

	#region  Private variable
	private ITurnBasedCombat _turnBasedCombat;
	private LamplighterCharacterController _controller;
	private AnimationPlayer _animationPlayer;
	#endregion 

	#region Public variable
	public ICombatActor CombatActor => _controller.CombatActor;
	#endregion

	

	
	#region Methods
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

	public void BootstrapInit(ITurnBasedCombatFactory turnBasedCombatFactory, IMovementService movementService)
	{
		DebugLamplighter.Assert(turnBasedCombatFactory != null, "turnBasedCombat is null");
		

		_turnBasedCombat = turnBasedCombatFactory.Create(_controller.CombatActor.AtbProperties.CharacterType);

		_turnBasedCombat.Init(new TurnBasedCombatContext(_controller.CombatBrain,
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

	public override void _ExitTree()
	{
		_turnBasedCombat.Dispose();
		base._ExitTree();
	}

	#endregion
}


