using System;

namespace SystemLamplighter.DataStructure
{
	public class MagicAction : ActionData
	{
		// Tipo di magia
		private MagicType _magicType;
		// Costo del mana
		private int _manaCost;
		// Danno della magia
		private float _damage;
		private float _pushbackForce;
		// Quanta posizione sottrae l'attacco sull'atb
		private float _atbPositionDamage;

		public MagicAction(MagicType magicType, int manaCost, float damage, float pushbackForce, float atbPositionDamage)
		{
			_magicType = magicType;
			_manaCost = manaCost;
			_damage = damage;
			_pushbackForce = pushbackForce;
			_atbPositionDamage = atbPositionDamage;
		}

	}
}