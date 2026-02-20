using Characters.Interfaces;

namespace SystemLamplighter.DataStructure.GeneralData;

public class ActorCursorState : TargetCursorState
{
	public ICombatActor Actor { get; private set; }

	public ActorCursorState(ICombatActor actor)
	{
		Actor = actor;
	}
}