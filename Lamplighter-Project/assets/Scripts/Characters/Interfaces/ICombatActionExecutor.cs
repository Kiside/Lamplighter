using Characters.Interfaces;
using SystemLamplighter.Events;

namespace Characters.Inteaces
{
	public interface ICombatActionExecutor
	{
		void OnExecuteCombatAction (AtbExecuteActionEvent ev);
	}
}