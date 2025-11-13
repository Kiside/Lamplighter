using System;
using SystemLamplighter;
using SystemLamplighter.DataStructure;

public class AttackAction : ActionData
{
	// Tipologia di attacco
	private AttackType _attackType;
	// Il danno che fa l'attacco
	private float _damage;
	// Se l'attacco ha una forza di pushback
	private float _pushbackForce;
	// Quanta posizione sottrae l'attacco sull'atb
	private float _atbPositionDamage;

	public AttackAction(string name, AttackType attackType, float damage, float pushbackForce, float atbPositionDamage, float actionSpeed)
	{
		_name = name;
		_attackType = attackType;
		_damage = damage;
		_pushbackForce = pushbackForce;
		_atbPositionDamage = atbPositionDamage;
		_actionSpeed = actionSpeed;
	}
}