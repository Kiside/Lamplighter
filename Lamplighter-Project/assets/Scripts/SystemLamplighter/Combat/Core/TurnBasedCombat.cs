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


namespace SystemLamplighter.Combat.Core
{
	public class TurnBasedCombat : ITurnBasedCombat, ICombatCommandHandler, ICombatActionExecutor
	{
		public BattleMenuController _battleMenuController {get; private set;}
		public ICombatActor Actor {get; set;}
		public CombatLoadout CombatLoadout {get => Actor.CombatLoadout;}
		public AtbCharacterProperties AtbProperties {get => Actor.AtbProperties;}
		public IActionData CurrentAction { get => Actor.CurrentAction; set => Actor.CurrentAction = value;}
		
		private IPublisher<AtbCommandPhaseEndEvent> _publishCommandPhaseEnd;
		private IPublisher<AtbEndExecuteActionEvent> _publishEndExecuteAction;
		private IPublisher<StartTargetEvent> _publisherStartTarget;
		private ISubscriber<AtbCommandPhaseStartedEvent> _subscriberCommandPhaseStarted;
		private ISubscriber<AtbExecuteActionEvent> _subscriberExecuteActionEvent;
		private ISubscriber<EndTargetEvent> _subscriberEndTarget;
		private IDisposable _disposeEndTargetEvent;
		private readonly DisposableBagBuilder _bag;

		public TurnBasedCombat(BattleMenuController battleMenuController, 
		ICombatActor combatActor, 
		IPublisher<AtbCommandPhaseEndEvent> publishCommandPhaseEnd,
		IPublisher<AtbEndExecuteActionEvent> publishEndExecuteAction,
		IPublisher<StartTargetEvent> publisherStartTarget,
		ISubscriber<AtbCommandPhaseStartedEvent> subscriberCommandPhaseStarted,
		ISubscriber<AtbExecuteActionEvent> subscriberExecuteAction,
		ISubscriber<EndTargetEvent> subscriberEndTarget)
		{
			_battleMenuController = battleMenuController;
			Actor = combatActor;
			_publishCommandPhaseEnd = publishCommandPhaseEnd;
			_publishEndExecuteAction = publishEndExecuteAction;
			_publisherStartTarget = publisherStartTarget;
			_subscriberCommandPhaseStarted = subscriberCommandPhaseStarted;
			_subscriberExecuteActionEvent = subscriberExecuteAction;
			_subscriberEndTarget = subscriberEndTarget;

			_bag = DisposableBag.CreateBuilder();

			_subscriberCommandPhaseStarted.Subscribe(OnCommandPhaseStarted).AddTo(_bag);
			_subscriberExecuteActionEvent.Subscribe(OnExecuteCombatAction).AddTo(_bag);

			_battleMenuController.OnActionClick += ActionChoosedHandler;
			_battleMenuController.OnOpenSubMenu += OpenBattleSubMenuHandler;
		}

		#region EVENTS HANDLER

		public void OnExecuteCombatAction(AtbExecuteActionEvent ev)
		{
			if (ev.Actor != Actor)
				return;

			// TODO Eseguire l'azione
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
			if (ev.TargetResolutionContext.CasterActor != Actor)
				return;

			_disposeEndTargetEvent?.Dispose();
			AtbProperties.EndCommandStatus(CurrentAction.ActionSpeedMultiplier);
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
