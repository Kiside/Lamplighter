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

namespace SystemLamplighter
{
	public class TurnBasedCombat : ITurnBasedCombat
	{
		public IActionData CurrentAction { get; set; }
		public BattleMenuController _battleMenuController {get; set;}
		public CombatLoadout CombatLoadout {get; set;}
		public AtbCharacterProperties AtbProperties {get; set;}

		public void OpenBattleSubMenuHandler(int subMenuType)
		{
			List<IActionData> subMenuIds = new List<IActionData>();
			SubMenuType type;
            if (Enum.IsDefined(typeof(SubMenuType), subMenuType))
            {
                type = (SubMenuType)subMenuType;

				switch (type)
				{
					case SubMenuType.ATTACK:
						subMenuIds = GetAttacksId();
					break;
					case SubMenuType.MAGIC:
						subMenuIds = GetMagicsId();
					break;
					case SubMenuType.ITEMS:
						subMenuIds = GetItemsId();
					break;
					case SubMenuType.DEFEND:
						CurrentAction = GetDefenseId().First();
						//this.PublishEvent<AtbCommandPhaseEndEvent>(new AtbCommandPhaseEndEvent(this));
						return;
				}

			 _battleMenuController.OpenSubMenu(type, subMenuIds);
            }
			else
			{
				Log.PrintError($"Value {subMenuType} has no enum defined in SubMenuType");
			}
			
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
