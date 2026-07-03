using System.Collections.Generic;
using System.Numerics;
using Characters.Interfaces;
namespace SystemLamplighter.Events;

/// <summary>
/// Classe per l'evento di fine fase target
/// </summary>
public sealed class EndTargetEvent
{
	public TargetResolutionContext TargetResolutionContext { get; private set; }
	public EndTargetEvent(TargetResolutionContext targetResolutionContext)
	{
		TargetResolutionContext = targetResolutionContext;
	}
}