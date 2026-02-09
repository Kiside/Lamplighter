using System;
using System.Diagnostics;
using Godot;
using SystemLamplighter;
using SystemLamplighter.DataStructure;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.DataStructure.GeneralData;
using SystemLamplighter.DataStructure.EffectsData;

namespace SystemLamplighter.DataStructure.ActionsData;

/// <summary>
/// Classe che rappresenta i dati di un attacco
/// </summary>
[GlobalClass]
[Serializable]
public partial class AttackAction : EquipableActionData
{
	[Export]
	// Tipologia di attacco
	private AttackType _attackType;
	public AttackType AttackType => _attackType;

	public AttackAction() : 
	this(ActionType.ATTACK, "", 1f, new TargetData(), null, new Godot.Collections.Array<EffectData>(), false, AttackType.LIGHT) { }

	public AttackAction(ActionType actionType, 
		string name, 
		float actionSpeedMultiplier, 
		TargetData targetData,
		Animation animation,
		Godot.Collections.Array<EffectData> effects,
		bool isEquipped,
		AttackType attackType) 
		: base(actionType, name, actionSpeedMultiplier, targetData, animation, effects, false)
	{
		_actionType = ActionType.ATTACK;
	}
}