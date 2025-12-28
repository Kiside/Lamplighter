using Characters.Interfaces;

public sealed class AtbCommandPhaseStartedEvent
{
	public ICombatActor Actor {get;}

	public AtbCommandPhaseStartedEvent(ICombatActor actor)
	{
		Actor = actor;
	}
}