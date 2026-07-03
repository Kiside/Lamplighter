using Characters.Interfaces;
using SystemLamplighter.Interfaces;

namespace SystemLamplighter.Interfaces;

//TODO: Da cancellare?
public interface IActionExecutor
{
	void ExecuteAction(IActionData action, ICombatActor actor);
}