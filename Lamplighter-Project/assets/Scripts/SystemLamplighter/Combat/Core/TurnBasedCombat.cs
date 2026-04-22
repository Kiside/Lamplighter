using SystemLamplighter.BattleMenu;
using Characters.Interfaces;
using MessagePipe;
using SystemLamplighter.Events;
using System.Linq;
using SystemLamplighter.DataStructure.ActionsData;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.Tool;
using SystemLamplighter.ATB;
using SystemLamplighter.Interfaces;
using Characters.Loadout;
using System.Runtime.InteropServices;
using System;
using SystemLamplighter.DataStructure.GeneralData;
using SystemLamplighter.Debug;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;


namespace SystemLamplighter.Combat.Core
{
	public class TurnBasedCombat : ITurnBasedCombat, ICombatCommandHandler, ICombatActionExecutor
	{
		public BattleMenuController _battleMenuController {get; private set;}
		// TODO: Controllare se Actor serve nel contesto, dato che viene passato più volte anche da altre parti
		public ICombatActor Actor {get; private set;}
		public CombatLoadout CombatLoadout {get => Actor.CombatLoadout;}
		public AtbCharacterProperties AtbProperties {get => Actor.AtbProperties;}
		public IActionData CurrentAction { get => Actor.CurrentAction; set => Actor.CurrentAction = value;}
		public TargetResolutionContext TargetResolutionContext {get; private set;}

		private readonly TurnBasicMovementResolver _movementResolver;
		private readonly ITargetableProvider _targetableProvider;
	
		private IPublisher<AtbCommandPhaseEndEvent> _publishCommandPhaseEnd;
		private IPublisher<AtbEndExecuteActionEvent> _publishEndExecuteAction;
		private IPublisher<StartTargetEvent> _publisherStartTarget;
		private ISubscriber<AtbCommandPhaseStartedEvent> _subscriberCommandPhaseStarted;
		private ISubscriber<AtbExecuteActionEvent> _subscriberExecuteActionEvent;
		private ISubscriber<EndTargetEvent> _subscriberEndTarget;
		private IDisposable _disposeEndTargetEvent;
		private readonly DisposableBagBuilder _bag;

		public Queue<Godot.Vector3> CombatMovement {get; private set;} 

		public TurnBasedCombat(TurnBasicMovementResolver movementResolver, ITargetableProvider targetableProvider)
		{
			_movementResolver = movementResolver;
			_targetableProvider = targetableProvider;

			_bag = DisposableBag.CreateBuilder();
		}

		public void Init(TurnBasedCombatContext turnBasedCombatContext)
		{
			_battleMenuController = turnBasedCombatContext.BattleMenuController;
			Actor = turnBasedCombatContext.Actor;
			_publishCommandPhaseEnd = turnBasedCombatContext.PublishCommandPhaseEnd;
			_publishEndExecuteAction = turnBasedCombatContext.PublishEndExecuteAction;
			_publisherStartTarget = turnBasedCombatContext.PublisherStartTarget;
			_subscriberCommandPhaseStarted = turnBasedCombatContext.SubscriberCommandPhaseStarted;
			_subscriberExecuteActionEvent = turnBasedCombatContext.SubscriberExecuteAction;
			_subscriberEndTarget = turnBasedCombatContext.SubscriberEndTarget;

			Log.PrintMessage($"subscribed phase command started {_subscriberCommandPhaseStarted}");

			_subscriberCommandPhaseStarted.Subscribe(OnCommandPhaseStarted).AddTo(_bag);
			_subscriberExecuteActionEvent.Subscribe(OnExecuteCombatAction).AddTo(_bag);

			_battleMenuController.OnActionClick += ActionChoosedHandler;
			_battleMenuController.OnOpenSubMenu += OpenBattleSubMenuHandler;

			CombatMovement = new Queue<Godot.Vector3>();
		}

		#region EVENTS HANDLER

		/// <summary>
		/// Metodo chiamato quando verrà eseguita l'azione
		/// </summary>
		/// <param name="ev"></param>
		public void OnExecuteCombatAction(AtbExecuteActionEvent ev)
		{
			if (ev.Actor != Actor)
				return;

			// TODO Eseguire l'azione
			// Bisogna capire la distanza dal target
			var casterPos = _targetableProvider.GetTargetable(ev.Actor.Id).Position;
			float maxDistanceFromTarget = 0f;
			var targetables = TargetResolutionContext.Targetables;


			foreach(var target in targetables)
			{
				var distanceFromTarget = casterPos.DistanceSquaredTo(target.Position);
				maxDistanceFromTarget = distanceFromTarget > maxDistanceFromTarget ? distanceFromTarget : maxDistanceFromTarget;
			}
			// Confrontare la distanza dal target con il range dell'attacco
			if(CurrentAction.TargetData.Range != 0f && maxDistanceFromTarget > CurrentAction.TargetData.Range)
			{
				var m = _movementResolver.ResolveMovementInRange(casterPos, targetables[0].Position, CurrentAction.TargetData.Range);
				CombatMovement = new Queue<Godot.Vector3>(m);
			}
			else
			{
				// In altri casi non bisogna muoversi
				var m = _movementResolver.ResolveMovement(casterPos, targetables[0].Position);
				CombatMovement = new Queue<Godot.Vector3>(m);
			}

			// Ad azione eseguita resetto la posizione del personaggio sull'ATB
			_publishEndExecuteAction.Publish(new AtbEndExecuteActionEvent(Actor));
		}

		public void OnCommandPhaseStarted(AtbCommandPhaseStartedEvent evt)
		{
			if (evt.Actor != Actor)
				return;

			// TODO: forse non deve essere qui che si gestisce tale evento
			_battleMenuController.Show();
		}

		public void OnEndTarget(EndTargetEvent ev)
		{
			DebugLamplighter.Assert(ev != null, "ev is null");
			DebugLamplighter.Assert(ev.TargetResolutionContext != null, "TargetResolutionContext is null");

			if (ev.TargetResolutionContext.CasterActor != Actor)
				return;

			TargetResolutionContext = null;
			_disposeEndTargetEvent?.Dispose();
			AtbProperties.EndCommandStatus(CurrentAction.ActionSpeedMultiplier);
			TargetResolutionContext = ev.TargetResolutionContext;
			_publishCommandPhaseEnd.Publish(new AtbCommandPhaseEndEvent(Actor));
		}

		public void OpenBattleSubMenuHandler(ISubMenuDefinition subMenuIds)
		{
			if (subMenuIds.IsImmediate)
			{
				CurrentAction = subMenuIds.BuildAction(Actor).First();
				_publishCommandPhaseEnd.Publish(new AtbCommandPhaseEndEvent(Actor));
				return;
			}

			var actions = subMenuIds.BuildAction(Actor);
			_battleMenuController.OpenSubMenu(actions);

		}

		public void ActionChoosedHandler(IActionData action)
		{
			CurrentAction = action;

			_disposeEndTargetEvent =_subscriberEndTarget.Subscribe(OnEndTarget);
			_publisherStartTarget.Publish(new StartTargetEvent(Actor, CurrentAction));

			// Che tipo di azione è? In base alla tipologia di azione ci saranno "cose da fare"
			switch (CurrentAction.ActionType)
			{
				case ActionType.ATTACK:
					HandleAttackAction();
					break;
				case ActionType.GUARD:
					HandleGuardAction();
					break;
				case ActionType.MAGIC:
					HandleMagicAction();
					break;
				case ActionType.ITEM:
					HandleItemAction();
					break;
				case ActionType.ESCAPE:
					HandleEscapeAction();
					break;
			}

			

			//_publishCommandPhaseEnd.Publish(new AtbCommandPhaseEndEvent(Actor));
		}

		#endregion
		

		public void HandleAttackAction()
		{
			if (CurrentAction is AttackAction action)
			{
				switch (action.AttackType)
				{
					case AttackType.AREA:
						break;
					case AttackType.PUSH:
						break;
					// Il caso di default è per tutte le tipologie di attacco che hanno come selezione un singolo target
					default:
						Log.PrintMessage("HANDLE ATTACK ACTION");
						//SingleTargetAttack();
						break;
				}
			}
		}
		public void HandleGuardAction()
		{ }
		public void HandleMagicAction()
		{ }
		public void HandleItemAction()
		{ }
		public void HandleEscapeAction()
		{ }

		public void Dispose()
		{
			_bag.Build().Dispose();

			_disposeEndTargetEvent?.Dispose();

			_battleMenuController.OnActionClick -= ActionChoosedHandler;
			_battleMenuController.OnOpenSubMenu -= OpenBattleSubMenuHandler;
		}
	}
}
