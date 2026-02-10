using Characters.Interfaces;

public partial class EndTargetEvent
{
	public ICombatActor Actor { get; private set}
	public EndTargetEvent(ICombatActor actor) => Actor = actor;
}