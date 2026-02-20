using System.Collections.Generic;
using Characters.Interfaces;
using Godot;

public class TargetResolutionContext
{
	private List<Vector3> _targetsPosition;
	private ICombatActor _casterActor;

	public TargetResolutionContext(List<Vector3> targetsPosition, ICombatActor casterActor)
	{
		_targetsPosition = targetsPosition;
		_casterActor = casterActor;
	}

	public TargetResolutionContext()
	{
		
	}
}