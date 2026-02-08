using System;
using Godot;
using SystemLamplighter.DataStructure;
using System.Diagnostics;
using System.Collections.Generic;
using SystemLamplighter.Interfaces;

namespace Characters.Loadout
{
	/// <summary>
	/// Classe per gestire l'equipaggiamento di skills(attacchi, magie e abilità) e equipaggiamento di un personaggio
	/// </summary>
	public class CharacterLoadout
	{
		CombatLoadout _combatLoadout;
		EquipmentLoadout _equipmentLoadout;

		public List<IActionData> GetAttacks() => _combatLoadout.GetAttacksId();
		public List<IActionData> GetMagics() => _combatLoadout.GetMagicsId();
		public List<IActionData> GetItems() => _combatLoadout.GetItemsId();




		public CharacterLoadout(CombatLoadout combatLoadout, EquipmentLoadout equipmentLoadout)
		{
			_combatLoadout = combatLoadout;
			_equipmentLoadout = equipmentLoadout;
		}

	}	


}
