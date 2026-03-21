using System.Collections.Generic;
using Characters.Interfaces;
using SystemLamplighter.Common.Enums;

namespace SystemLamplighter.DataStructure.GeneralData;

public class ActorCursorState : TargetCursorState
{
	public List<ITargetable> TargetablesSelected { get; private set; }
	public ITargetable TargetableFocused { get; private set; }
	public List<TargeTableType> WhoTarget {get; private set;}

	public ActorCursorState() : base(TargetResolutionStatus.CANCELED) {}

	public ActorCursorState(ITargetable targetableFocused, List<TargeTableType> whoTarget) : base(TargetResolutionStatus.ON_GOING)
	{
		TargetablesSelected = new List<ITargetable>();
		TargetableFocused = targetableFocused;
		WhoTarget = whoTarget;
	}

	public ActorCursorState(List<ITargetable> targetablesSelected, 
	ITargetable targetableFocused, 
	List<TargeTableType> whoTarget) : base(TargetResolutionStatus.ON_GOING)
	{
		TargetablesSelected = targetablesSelected;
		TargetableFocused = targetableFocused;
		WhoTarget = whoTarget;
	}

	public ActorCursorState(List<ITargetable> targetablesSelected, 
	ITargetable targetableFocused, 
	List<TargeTableType> whoTarget,
	TargetResolutionStatus targetResolutionStatus) : base(targetResolutionStatus)
	{
		TargetablesSelected = targetablesSelected;
		TargetableFocused = targetableFocused;
		WhoTarget = whoTarget;
	}
}