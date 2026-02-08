using Characters.Interfaces;

namespace SystemLamplighter.Interfaces;

/// <summary>
/// Servizio per la risoluzione dei target
/// </summary>
public interface ITargetService
{
	public void ResolveTargets(IActionData action, ICombatActor mainActor);
}