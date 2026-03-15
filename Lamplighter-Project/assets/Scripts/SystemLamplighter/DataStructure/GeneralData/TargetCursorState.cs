namespace SystemLamplighter.DataStructure.GeneralData;

public abstract class TargetCursorState
{
	public TargetResolutionStatus TargetResolutionStatus { get; protected set; }

	public TargetCursorState(TargetResolutionStatus targetResolutionStatus)
	{
		TargetResolutionStatus = targetResolutionStatus;
	}
}