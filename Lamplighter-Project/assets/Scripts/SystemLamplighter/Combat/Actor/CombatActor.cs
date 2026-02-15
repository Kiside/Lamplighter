using Characters;
using Characters.Interfaces;
using SystemLamplighter;
using Characters.Loadout;
using SystemLamplighter.ATB;
using SystemLamplighter.Interfaces;
using SystemLamplighter.Common.Enums;
using System.Numerics;
using System;

namespace SystemLamplighter.Combat.Actor;

public class CombatActor : ICombatActor
{
	private IActionData _currentAction;
	public AtbCharacterProperties AtbProperties {get; private set;}
	public AtbCharacterStatus AtbStatus {get; private set;}
	public CombatLoadout CombatLoadout {get; private set;}
	public IActionData CurrentAction {get => _currentAction; set {_currentAction = value;}}

	public CombatActor(AtbCharacterProperties atbCharacterProperties, CombatLoadout combatLoadout)
	{
		AtbProperties = atbCharacterProperties;
		CombatLoadout = combatLoadout;
	}

	public AtbCharacterStatus UpdateAtbPosition(float value) => AtbProperties.UpdatePosition(value);
}