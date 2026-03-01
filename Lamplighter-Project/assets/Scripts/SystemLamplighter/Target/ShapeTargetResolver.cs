using Characters.Interfaces;
using Godot;
using SystemLamplighter.Interfaces;
using SystemLamplighter.DataStructure.GeneralData;


namespace SystemLamplighter.Target;

public class ShapeTargetResolver : TargetResolver
{
	public override TargetCursorState ResolveTargets(IActionData action, ICombatActor mainActor)
	{
		return new PositionCursorState(new Vector3(0,0,0)); 
	}

	public override TargetCursorState MoveTarget(Vector2 direction)
	{
		return new PositionCursorState(new Vector3(0,0,0));
	}

	public override TargetResolutionData Select()
	{
		return new TargetResolutionData(
				new TargetResolutionContext(),
				TargetResolutionStatus.ON_GOING
			);
	}
	public override TargetResolutionData Cancel()
	{
		return new TargetResolutionData(
				new TargetResolutionContext(),
				TargetResolutionStatus.CANCELED
			);
	}
}