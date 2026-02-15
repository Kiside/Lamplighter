using System.Collections.Generic;
using System.Numerics;
using Characters.Interfaces;

public partial class EndTargetEvent
{
	public List<Vector3> TargetsPosition { get; private set;}
	public ICombatActor Actor { get; private set; }
	public EndTargetEvent(List<Vector3> targetsPosition, ICombatActor actor)
	{
		 TargetsPosition = targetsPosition;
		 Actor = actor;
	}
}