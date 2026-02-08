using Characters.Interfaces;
namespace SystemLamplighter.Events;

/// <summary>
/// Classe per l'evento di inizio fase di comando
/// </summary>
public sealed class AtbCommandPhaseStartedEvent
{
	public ICombatActor Actor {get;}

	public AtbCommandPhaseStartedEvent(ICombatActor actor)
	{
		Actor = actor;
	}
}