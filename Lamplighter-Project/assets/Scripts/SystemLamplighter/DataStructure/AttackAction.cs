using System;
using System.Diagnostics;
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
		Debug.Assert(name != String.Empty, "name is empty");
		Debug.Assert(damage > 0, "damage is negative");
		Debug.Assert(pushbackForce > 0, "pushbackForce is negative");
		Debug.Assert(atbPositionDamage > 0, "atbPositionDamage is negative");
		Debug.Assert(actionSpeed > 0, "actionSpeed is negative");
		_actionType = ActionType.ATTACK;
		_name = name;
		_attackType = attackType;
		_damage = damage;
		_pushbackForce = pushbackForce;
		_atbPositionDamage = atbPositionDamage;
		_actionSpeed = actionSpeed;
	}
}