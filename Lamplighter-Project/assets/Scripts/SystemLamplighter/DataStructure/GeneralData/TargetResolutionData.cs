using Characters.Interfaces;
using SystemLamplighter.Interfaces;

namespace SystemLamplighter.DataStructure.GeneralData;

public class TargetResolutionData
{
	public TargetResolutionStatus TargetResolutionStatus { get; private set; }
	public TargetResolutionContext TargetResolutionContext { get; private set; }

	public TargetResolutionData(TargetResolutionContext targetResolutionContext, TargetResolutionStatus targetResolutionStatus)
	{
		TargetResolutionStatus = targetResolutionStatus;
		TargetResolutionContext = targetResolutionContext;
	}
}