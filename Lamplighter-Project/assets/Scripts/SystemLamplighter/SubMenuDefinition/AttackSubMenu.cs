using System.Collections.Generic;
using Characters.Interfaces;
using SystemLamplighter.Interfaces;
using SystemLamplighter.Common.Enums;

namespace SystemLamplighter.SubMenuDefinition;

/// <summary>
/// Classe per le tipologie di sottomenu di attacco
/// </summary>
public class AttackSubMenu : ISubMenuDefinition
{
	public string Id => SubMenuType.ATTACK.ToString();
	public string DisplayName => "Attack";
	public bool IsImmediate => false;

	public IReadOnlyList<IActionData> BuildAction(ICombatActor actor) => actor.CombatLoadout.GetAttacksId();
}