using System.Collections.Generic;
using Characters.Interfaces;

namespace SystemLamplighter.Interfaces;

/// <summary>
/// Interfaccia per la definizione di un sottomenu
/// </summary>
public interface ISubMenuDefinition
{
	string Id {get;}
	string DisplayName {get;}
	bool IsImmediate {get;}

	IReadOnlyList<IActionData> BuildAction(ICombatActor actor);
}