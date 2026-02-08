using System.Collections.Generic;
using Characters.Interfaces;
using SystemLamplighter.Interfaces;
using SystemLamplighter.Common.Enums;

namespace SystemLamplighter.SubMenuDefinition;

/// <summary>
/// Classe per la tipologia di sottomenu di magia
/// </summary>
public class MagicSubMenu : ISubMenuDefinition
{
	public string Id => SubMenuType.MAGIC.ToString();
	public string DisplayName => "Magic";
	public bool IsImmediate => false;

	public IReadOnlyList<IActionData> BuildAction(ICombatActor actor) => actor.CombatLoadout.GetMagicsId();
}