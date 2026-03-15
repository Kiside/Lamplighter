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

	public override TargetCursorState Select()
	{
		throw new System.NotImplementedException();
	}
	public override TargetCursorState Cancel()
	{
		throw new System.NotImplementedException();
	}

	public override void Dispose()
	{
		
	}
}