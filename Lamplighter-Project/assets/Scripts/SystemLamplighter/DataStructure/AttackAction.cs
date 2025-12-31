using System;
using System.Diagnostics;
using Godot;
using SystemLamplighter;
using SystemLamplighter.DataStructure;

[GlobalClass]
[Serializable]
public partial class AttackAction : EquipableActionData
{
	[Export]
	// Tipologia di attacco
	private AttackType _attackType;
	// Il danno che fa l'attacco
	[Export]
	private float _damage;
	[Export]
	// Se l'attacco ha una forza di pushback
	private float _pushbackForce;
	[Export]
	// Quanta posizione sottrae l'attacco sull'atb
	private float _atbPositionDamage;

	public AttackAction() : this("", AttackType.LIGHT, 0f, 0f, 0f, 1f, false) { }

	public AttackAction(string name, AttackType attackType, float damage, float pushbackForce, float atbPositionDamage, float actionSpeed, bool isEquipped)
	{
		_actionType = ActionType.ATTACK;
		_name = name;
		_attackType = attackType;
		_damage = damage;
		_pushbackForce = pushbackForce;
		_atbPositionDamage = atbPositionDamage;
		_actionSpeedMultiplier = actionSpeed;
		_isEquipped = isEquipped;
	}
}