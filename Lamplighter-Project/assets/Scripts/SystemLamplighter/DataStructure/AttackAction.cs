using System;
using Godot;
using SystemLamplighter;
using SystemLamplighter.DataStructure;

[GlobalClass]
[Serializable]
public partial class AttackAction : ActionData
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

	public AttackAction() : this("", AttackType.LIGHT, 0f, 0f, 0f, 0f) { }

	public AttackAction(string name, AttackType attackType, float damage, float pushbackForce, float atbPositionDamage, float actionSpeed)
	{
		_actionType = ActionType.ATTACK;
		_name = name;
		_attackType = attackType;
		_damage = damage;
		_pushbackForce = pushbackForce;
		_atbPositionDamage = atbPositionDamage;
		_actionSpeed = actionSpeed;
	}
}