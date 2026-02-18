using System.Collections.Generic;
using Characters.Interfaces;
using Godot;

public class TargetResolutionContext
{
	private List<Vector3> _targetsPosition;
	private ICombatActor casterActor;

	public TargetResolutionContext(List<Vector3> targetsPosition, ICombatActor actor)
	{
		_targetsPosition = targetsPosition;
		casterActor = actor;
	}
}