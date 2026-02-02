using Characters.Interfaces;

public interface ITargetService
{
	public void ResolveTargets(IActionData action, ICombatActor mainActor);
}