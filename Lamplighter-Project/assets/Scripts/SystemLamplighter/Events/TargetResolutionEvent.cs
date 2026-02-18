using Characters.Interfaces;
using SystemLamplighter.Interfaces;

public sealed class TargetResolutionEvent
{
	public TargetResolutionStatus TargetResolutionStatus { get; private set; }
	public TargetResolutionContext TargetResolutionContext { get; private set; }

	public TargetResolutionEvent(TargetResolutionContext targetResolutionContext, TargetResolutionStatus targetResolutionStatus)
	{
		TargetResolutionStatus = targetResolutionStatus;
		TargetResolutionContext = targetResolutionContext;
	}
}