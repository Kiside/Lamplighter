using Characters.Interfaces;
using SystemLamplighter.Interfaces;

namespace SystemLamplighter.Events;

/// <summary>
/// Classe per l'evento di inizio Fase di target
/// </summary>
public sealed class StartTargetEvent
{
	public ICombatActor Actor { get; private set;}
	public IActionData Action { get; private set; }

	public StartTargetEvent(ICombatActor actor, IActionData action)
	{
		Actor = actor;
		Action = action;
	}
}