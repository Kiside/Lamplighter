using System.Collections.Generic;
using Characters.Interfaces;
using SystemLamplighter;

public class ItemSubMenu : ISubMenuDefinition
{
	public string Id => SubMenuType.ITEMS.ToString();
	public string DisplayName => "Items";
	public bool IsImmediate => false;

	public IReadOnlyList<IActionData> BuildAction(ICombatActor actor) => actor.CombatLoadout.GetItemsId();
}