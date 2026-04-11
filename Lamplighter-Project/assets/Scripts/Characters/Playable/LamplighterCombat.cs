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

		DebugLamplighter.Assert(_turnBasedCombat != null, "_turnBasedCombat is null");

		var sub = this.GetSubscriber<AtbCommandPhaseStartedEvent>();

		_turnBasedCombat.Init(new TurnBasedCombatContext(controller.BattleMenuController,
				controller.CombatActor,
				this.GetPublisher<AtbCommandPhaseEndEvent>(),
				this.GetPublisher<AtbEndExecuteActionEvent>(),
				this.GetPublisher<StartTargetEvent>(),
				sub,
				this.GetSubscriber<AtbExecuteActionEvent>(),
				this.GetSubscriber<EndTargetEvent>()));


	}

	public void BootstrapInit(ITurnBasedCombat turnBasedCombat)
	{
		_turnBasedCombat = turnBasedCombat;
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


