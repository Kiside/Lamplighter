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
	public AttackType AttackType => _attackType;

	public AttackAction() : this("", AttackType.LIGHT, 1f, false) { }

	public AttackAction(string name, AttackType attackType, float actionSpeed, bool isEquipped)
	{
		_actionType = ActionType.ATTACK;
		_name = name;
		_attackType = attackType;
		_actionSpeedMultiplier = actionSpeed;
		_isEquipped = isEquipped;
	}
}