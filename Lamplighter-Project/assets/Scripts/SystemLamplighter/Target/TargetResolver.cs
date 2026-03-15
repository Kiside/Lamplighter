using Characters.Interfaces;
using Godot;
using SystemLamplighter.Interfaces;
using SystemLamplighter.Target.Interfaces;
using SystemLamplighter.DataStructure.GeneralData;

namespace SystemLamplighter.Target;

public abstract class TargetResolver : ITargetResolver
{
	public bool IsActive { get; set; }
	
	public abstract TargetCursorState ResolveTargets(IActionData action, ICombatActor mainActor);

	public TargetResolver() : this(false) {}

	public TargetResolver(bool isActive)
	{
		IsActive = isActive;
	}

	public abstract TargetCursorState Select();
	public abstract TargetCursorState Cancel();
	public abstract TargetCursorState MoveTarget(Vector2 direction);

	public abstract void Dispose();
}