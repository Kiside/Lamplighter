using System.Collections.Generic;
using Characters.Interfaces;
using SystemLamplighter;

namespace SystemLamplighter.SubMenuDefinition;

// ? SERVE QUESTA CLASSE? 
/// <summary>
/// Sotto menu per scappare
/// </summary>
public class RunSubMenu //: ISubMenuDefinition
{
	public string Id => "RUN";
	public string DisplayName => "Run";
	public bool IsImmediate => true;

	//public IReadOnlyList<IActionData> BuildAction(ICombatActor actor) => new IReadOnlyList<IActionData>();
}