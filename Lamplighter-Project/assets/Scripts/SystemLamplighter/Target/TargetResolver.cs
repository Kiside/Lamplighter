using Characters.Interfaces;
using Godot;
using SystemLamplighter.Interfaces;

namespace SystemLamplighter.Target;

public abstract partial class TargetResolver : Resource, SystemLamplighter.Target.Interfaces.ITargetResolver
{
	public bool IsActive { get; set; }
	
	public abstract void ResolveTargets(IActionData action, ICombatActor mainActor);
}