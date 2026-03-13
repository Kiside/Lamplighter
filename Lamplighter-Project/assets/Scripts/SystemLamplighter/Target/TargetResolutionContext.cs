using System.Collections.Generic;
using Characters.Interfaces;
using Godot;

public class TargetResolutionContext
{
	public List<ITargetable> Targetables { get; private set; }
	public ICombatActor CasterActor { get; private set; }

	public TargetResolutionContext(List<ITargetable> targetables, ICombatActor casterActor)
	{
		Targetables = targetables;
		CasterActor = casterActor;
	}

	public TargetResolutionContext()
	{
		
	}
}