using System.Collections.Generic;
using Characters.Interfaces;

namespace SystemLamplighter.DataStructure.GeneralData;

public class ActorCursorState : TargetCursorState
{
	public ITargetable TargetableSelected { get; private set; }

	public ActorCursorState(ITargetable targetable)
	{
		TargetableSelected = targetable;
	}
}