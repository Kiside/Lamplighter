using Characters.Interfaces;
using SystemLamplighter.Interfaces;

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