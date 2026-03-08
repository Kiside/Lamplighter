using System.Collections.Generic;
using Characters.Interfaces;

namespace SystemLamplighter.DataStructure.GeneralData;

public class ActorCursorState : TargetCursorState
{
	public ICombatActor ActorSelected { get; private set; }

	public ActorCursorState(ICombatActor actor, List<ICombatActor> actors)
	{
		ActorSelected = actor;
	}
}