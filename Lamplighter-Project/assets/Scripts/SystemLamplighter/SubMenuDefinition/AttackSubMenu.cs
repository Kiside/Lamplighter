using System.Collections.Generic;
using Characters.Interfaces;
using SystemLamplighter;

public class AttackSubMenu : ISubMenuDefinition
{
	public string Id => SubMenuType.ATTACK.ToString();
	public string DisplayName => "Attack";
	public bool IsImmediate => false;

	public IReadOnlyList<IActionData> BuildAction(ICombatActor actor) => actor.CombatLoadout.GetAttacksId();
}