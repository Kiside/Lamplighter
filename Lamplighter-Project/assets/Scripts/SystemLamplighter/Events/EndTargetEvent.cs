using System.Collections.Generic;
using System.Numerics;
using Characters.Interfaces;

public partial class EndTargetEvent
{
	public TargetResolutionContext TargetResolutionContext { get; private set; }
	public EndTargetEvent(TargetResolutionContext targetResolutionContext)
	{
		TargetResolutionContext = targetResolutionContext;
	}
}