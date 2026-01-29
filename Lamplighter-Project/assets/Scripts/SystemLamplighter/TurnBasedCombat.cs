using System.Collections.Generic;
using System.Diagnostics;
using SystemLamplighter;
using SystemLamplighter.BattleMenu;
using Characters.Interfaces;
using Characters.Inteaces;
using Microsoft.Extensions.DependencyInjection;
using MessagePipe;
using SystemLamplighter.Events;
using SystemLamplighter.Extensions;
using System.Linq;
using Characters;
using System;
using SystemLamplighter.Debug;
using System.Data;

namespace SystemLamplighter
{
	public class TurnBasedCombat : ITurnBasedCombat
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

			_subscriberCommandPhaseStarted.Subscribe(OnCommandPhaseStarted);
			_subscriberExecuteActionEvent.Subscribe(OnExecuteCombatAction);
		}

		public void OnExecuteCombatAction(AtbExecuteActionEvent ev)
		{
			if(ev.Actor != null)
				return;
			
			// Eseguo l'azione
			// Ad azione eseguita resetto la posizione del personaggio sull'ATB
			_publishEndExecuteAction.Publish(new AtbEndExecuteActionEvent(Actor));
		}

		public void OnCommandPhaseStarted(AtbCommandPhaseStartedEvent evt)
		{
			if(evt.Actor != this)
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

		// QUESTA REGION È IMPORTANTE PER I PLAYERS E FORSE ANCHE PER I CHARACTERS IN COMBATTIMENTO
		// TODO: CONTROLLARE SE POSSONO ESSERE MESSI ALTROVE QUESTI METODI, SEMPRE CLASSE/INTERFACCE
		#region COMBATLOADOUT METHODS
		public List<IActionData> GetAttacksId() => CombatLoadout.GetAttacksId();
		public List<IActionData> GetMagicsId() => CombatLoadout.GetMagicsId();
		public List<IActionData> GetItemsId() => CombatLoadout.GetItemsId();
		public List<IActionData> GetDefenseId() => CombatLoadout.GetDefenseId();
		#endregion

		public void ActionChoosedHandler()
		{
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

			//this.PublishEvent<AtbCommandPhaseEndEvent>(new AtbCommandPhaseEndEvent(this));
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
	}
}
