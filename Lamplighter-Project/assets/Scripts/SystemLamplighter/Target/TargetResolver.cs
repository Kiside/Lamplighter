using Characters.Interfaces;
using Godot;
using SystemLamplighter.Interfaces;
using SystemLamplighter.Target.Interfaces;

namespace SystemLamplighter.Target;

[GlobalClass]
public abstract partial class TargetResolver : Resource, ITargetResolver
{
	public bool IsActive { get; set; }
	
	public abstract void ResolveTargets(IActionData action, ICombatActor mainActor);

	public TargetResolver() : this(false) {}

	public TargetResolver(bool isActive)
	{
		IsActive = isActive;
	}
}