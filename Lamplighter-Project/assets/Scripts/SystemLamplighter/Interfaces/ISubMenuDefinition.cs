using System.Collections.Generic;
using Characters.Interfaces;

public interface ISubMenuDefinition
{
	string Id {get;}
	string DisplayName {get;}
	bool IsImmediate {get;}

	IReadOnlyList<IActionData> BuildAction(ICombatActor actor);
}