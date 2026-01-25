using System.Collections.Generic;
using System.Linq;
using Characters.Interfaces;
using SystemLamplighter;

public class DefendSubMenu : ISubMenuDefinition
{
	public string Id => SubMenuType.DEFEND.ToString();
	public string DisplayName => "Defend";
	public bool IsImmediate => true;

	public IReadOnlyList<IActionData> BuildAction(ICombatActor actor) => new [] {actor.CombatLoadout.GetDefenseId().First()};
}