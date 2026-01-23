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

namespace SystemLamplighter
{
	public class TurnBasedCombat : ITurnBasedCombat
	{
		public IActionData CurrentAction { get; set; }

		// public void OpenBattleSubMenuHandler(SubMenuType subMenuType)
		// {
		// 	List<IActionData> subMenuIds = new List<IActionData>();
		// 	switch (subMenuType)
		// 	{
		// 		case SubMenuType.ATTACK:
		// 			subMenuIds = GetAttacksId();
		// 			break;
		// 		case SubMenuType.MAGIC:
		// 			subMenuIds = GetMagicsId();
		// 			break;
		// 		case SubMenuType.ITEMS:
		// 			subMenuIds = GetItemsId();
		// 			break;
		// 		case SubMenuType.DEFEND:
		// 			CurrentAction = GetDefenseId().First();
		// 			this.PublishEvent<AtbCommandPhaseEndEvent>(new AtbCommandPhaseEndEvent(this));
		// 			return;
		// 	}

		// 	_battleMenuController.OpenSubMenu(subMenuType, subMenuIds);
		// }

		// public void ActionChoosedHandler()
		// {
		// 	// Che tipo di azione è? In base alla tipologia di azione ci saranno "cose da fare"
		// 	switch (CurrentAction.ActionType)
		// 	{
		// 		case ActionType.ATTACK:
		// 			HandleAttackAction();
		// 			break;
		// 		case ActionType.GUARD:
		// 			HandleGuardAction();
		// 			break;
		// 		case ActionType.MAGIC:
		// 			HandleMagicAction();
		// 			break;
		// 		case ActionType.ITEM:
		// 			HandleItemAction();
		// 			break;
		// 		case ActionType.ESCAPE:
		// 			HandleEscapeAction();
		// 			break;
		// 	}

		// 	AtbProperties.EndCommandStatus(CurrentAction.ActionSpeedMultiplier);

		// 	this.PublishEvent<AtbCommandPhaseEndEvent>(new AtbCommandPhaseEndEvent(this));
		// }

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
