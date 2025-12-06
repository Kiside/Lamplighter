using System;
using Godot;
using SystemLamplighter.DataStructure;
using System.Diagnostics;
using System.Collections.Generic;

namespace Characters
{
	/// <summary>
	/// Classe per gestire l'equipaggiamento di skills(attacchi, magie e abilità) e equipaggiamento di un personaggio
	/// </summary>
	public class CharacterLoadout
	{
		CombatLoadout _combatLoadout;
		EquipmentLoadout _equipmentLoadout;

		public List<string> GetAttacks() => _combatLoadout.GetAttacksId();
		public List<string> GetMagics() => _combatLoadout.GetMagicId();
		public List<string> GetItems() => _combatLoadout.GetItemsId();




		public CharacterLoadout(CombatLoadout combatLoadout, EquipmentLoadout equipmentLoadout)
		{
			_combatLoadout = combatLoadout;
			_equipmentLoadout = equipmentLoadout;
		}

	}	


}
