using System.Collections.Generic;
using Characters.Interfaces;

namespace SystemLamplighter.DataStructure.GeneralData;

public class ActorCursorState : TargetCursorState
{
	public List<ITargetable> TargetablesSelected { get; private set; }
	public ITargetable TargetableFocused { get; private set; }

	public ActorCursorState() : base(TargetResolutionStatus.CANCELED) {}

	public ActorCursorState(ITargetable targetableFocused) : base(TargetResolutionStatus.ON_GOING)
	{
		TargetablesSelected = new List<ITargetable>();
		TargetableFocused = targetableFocused;
	}

	public ActorCursorState(List<ITargetable> targetablesSelected, ITargetable targetableFocused, TargetResolutionStatus targetResolutionStatus) : base(targetResolutionStatus)
	{
		TargetablesSelected = targetablesSelected;
		TargetableFocused = targetableFocused;
	}
}