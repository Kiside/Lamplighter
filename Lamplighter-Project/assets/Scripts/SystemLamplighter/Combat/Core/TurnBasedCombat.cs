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
		private ISubscriber<AtbCommandPhaseStartedEvent> _subscriberCommandPhaseStarted;
		private ISubscriber<AtbExecuteActionEvent> _subscriberExecuteActionEvent;
		private readonly DisposableBagBuilder _bag;

		public TurnBasedCombat(BattleMenuController battleMenuController, 
		ICombatActor combatActor, 
		IPublisher<AtbCommandPhaseEndEvent> publishCommandPhaseEnd,
		IPublisher<AtbEndExecuteActionEvent> publishEndExecuteAction,
		ISubscriber<AtbCommandPhaseStartedEvent> subscriberCommandPhaseStarted,
		ISubscriber<AtbExecuteActionEvent> subscriberExecuteAction)
		{
			_battleMenuController = battleMenuController;
			Actor = combatActor;
			_publishCommandPhaseEnd = publishCommandPhaseEnd;
			_publishEndExecuteAction = publishEndExecuteAction;
			_subscriberCommandPhaseStarted = subscriberCommandPhaseStarted;
			_subscriberExecuteActionEvent = subscriberExecuteAction;

			_bag = DisposableBag.CreateBuilder();

			_subscriberCommandPhaseStarted.Subscribe(OnCommandPhaseStarted).AddTo(_bag);
			_subscriberExecuteActionEvent.Subscribe(OnExecuteCombatAction).AddTo(_bag);

			_battleMenuController.OnActionClick += ActionChoosedHandler;
			_battleMenuController.OnOpenSubMenu += OpenBattleSubMenuHandler;
		}

		public void OnExecuteCombatAction(AtbExecuteActionEvent ev)
		{
			if(ev.Actor != Actor)
				return;
			
			// Eseguo l'azione
			// Ad azione eseguita resetto la posizione del personaggio sull'ATB
			_publishEndExecuteAction.Publish(new AtbEndExecuteActionEvent(Actor));
		}

		public void OnCommandPhaseStarted(AtbCommandPhaseStartedEvent evt)
		{
			if(evt.Actor != Actor)
				return;

			// TODO: forse non deve essere qui che si gestisce tale evento
			_battleMenuController.Show();
		}

		public void OpenBattleSubMenuHandler(ISubMenuDefinition subMenuIds)
		{
			if(subMenuIds.IsImmediate)
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

			AtbProperties.EndCommandStatus(CurrentAction.ActionSpeedMultiplier);

			_publishCommandPhaseEnd.Publish(new AtbCommandPhaseEndEvent(Actor));
		}

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

			_battleMenuController.OnActionClick -= ActionChoosedHandler;
			_battleMenuController.OnOpenSubMenu -= OpenBattleSubMenuHandler;
		}
	}
}
