using Characters.Interfaces;
using SystemLamplighter.Interfaces;

public interface IActionExecutor
{
	void ExecuteAction(IActionData action, ICombatActor actor);
}