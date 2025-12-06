using System;
using System.Diagnostics;
using Godot;

namespace SystemLamplighter.DataStructure
{
	[GlobalClass]
	public partial class MagicAction : EquipableActionData
	{
		[Export]
		// Tipo di magia
		private MagicType _magicType;
		[Export]
		// Costo del mana
		private int _manaCost;
		[Export]
		// Danno della magia
		private float _damage;
		[Export]
		private float _pushbackForce;
		[Export]
		// Quanta posizione sottrae l'attacco sull'atb
		private float _atbPositionDamage;

		public MagicAction() : this(MagicType.AREA, 0, 0f, 0f, 0f, ActionType.MAGIC, false) { }

		public MagicAction(MagicType magicType, int manaCost, float damage, float pushbackForce, float atbPositionDamage, ActionType actionType, bool isEquipped)
		{
			_actionType = actionType;
			_magicType = magicType;
			_manaCost = manaCost;
			_damage = damage;
			_pushbackForce = pushbackForce;
			_atbPositionDamage = atbPositionDamage;
			_isEquipped = isEquipped;
		}

	}
}