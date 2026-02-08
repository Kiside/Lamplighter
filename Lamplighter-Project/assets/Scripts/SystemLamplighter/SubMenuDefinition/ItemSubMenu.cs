using System.Collections.Generic;
using Characters.Interfaces;
using SystemLamplighter.Interfaces;
using SystemLamplighter.Common.Enums;

namespace SystemLamplighter.SubMenuDefinition;

/// <summary>
/// Classe per la tipologia di sottomenu di item
/// </summary>
public class ItemSubMenu : ISubMenuDefinition
{
	public string Id => SubMenuType.ITEMS.ToString();
	public string DisplayName => "Items";
	public bool IsImmediate => false;

	public IReadOnlyList<IActionData> BuildAction(ICombatActor actor) => actor.CombatLoadout.GetItemsId();
}