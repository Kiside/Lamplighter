namespace SystemLamplighter.DataStructure.GeneralData;

/// <summary>
/// Classe astratta per il TargetCursorState
/// </summary>
public abstract class TargetCursorState
{
	public TargetResolutionStatus TargetResolutionStatus { get; protected set; }

	public TargetCursorState(TargetResolutionStatus targetResolutionStatus)
	{
		TargetResolutionStatus = targetResolutionStatus;
	}
}