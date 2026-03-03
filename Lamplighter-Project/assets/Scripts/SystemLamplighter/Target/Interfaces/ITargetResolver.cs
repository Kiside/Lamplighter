using System;
using Characters.Interfaces;
using SystemLamplighter.Interfaces;
using SystemLamplighter.DataStructure.GeneralData;
using Godot;


namespace SystemLamplighter.Target.Interfaces;

/// <summary>
/// Servizio per la risoluzione dei target
/// </summary>
public interface ITargetResolver : IDisposable
{
	public TargetCursorState ResolveTargets(IActionData action, ICombatActor mainActor);
	public abstract TargetCursorState MoveTarget(Vector2 direction);

	public TargetResolutionData Select();
	public TargetResolutionData Cancel();
}