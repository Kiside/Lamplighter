using System;
using System.Diagnostics;
using Godot;

namespace SystemLamplighter.DataStructure
{
	[GlobalClass]
	public partial class MagicAction : EquipableActionData
	{
		[Export]
		// Costo del mana
		private int _manaCost;

		public int ManaCost => _manaCost;

		public MagicAction() : this(0, ActionType.MAGIC, false) { }

		public MagicAction(int manaCost, ActionType actionType, bool isEquipped)
		{
			_actionType = actionType;
			_manaCost = manaCost;
			_isEquipped = isEquipped;
		}

	}
}