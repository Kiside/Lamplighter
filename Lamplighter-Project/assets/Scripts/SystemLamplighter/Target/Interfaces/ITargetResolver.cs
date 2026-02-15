using System;
using Characters.Interfaces;
using SystemLamplighter.Interfaces;

namespace SystemLamplighter.Target.Interfaces;

/// <summary>
/// Servizio per la risoluzione dei target
/// </summary>
public interface ITargetResolver : IDisposable
{
	public void ResolveTargets(IActionData action, ICombatActor mainActor);
}