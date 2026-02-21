using Characters.Interfaces;
using Godot;
using SystemLamplighter.Interfaces;
using SystemLamplighter.Target.Interfaces;
using SystemLamplighter.DataStructure.GeneralData;

namespace SystemLamplighter.Target;

[GlobalClass]
public abstract partial class TargetResolver : Resource, ITargetResolver
{
	public bool IsActive { get; set; }
	
	public abstract TargetCursorState ResolveTargets(IActionData action, ICombatActor mainActor);

	public TargetResolver() : this(false) {}

	public TargetResolver(bool isActive)
	{
		IsActive = isActive;
	}

	public abstract TargetResolutionData Select();
	public abstract TargetResolutionData Cancel();
	public abstract TargetCursorState MoveTarget(Vector2 direction);
}