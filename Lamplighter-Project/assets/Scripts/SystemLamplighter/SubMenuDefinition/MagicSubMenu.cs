using System.Collections.Generic;
using Characters.Interfaces;
using SystemLamplighter;

public class MagicSubMenu : ISubMenuDefinition
{
	public string Id => SubMenuType.MAGIC.ToString();
	public string DisplayName => "Magic";
	public bool IsImmediate => false;

	public IReadOnlyList<IActionData> BuildAction(ICombatActor actor) => actor.CombatLoadout.GetMagicsId();
}