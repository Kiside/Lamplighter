using System.Collections.Generic;
using Characters.Interfaces;
using Godot;

public class TargetResolutionContext
{
	public List<Vector3> TargetsPosition { get; private set; }
	public ICombatActor CasterActor { get; private set; }

	public TargetResolutionContext(List<Vector3> targetsPosition, ICombatActor casterActor)
	{
		TargetsPosition = targetsPosition;
		CasterActor = casterActor;
	}

	public TargetResolutionContext()
	{
		
	}
}