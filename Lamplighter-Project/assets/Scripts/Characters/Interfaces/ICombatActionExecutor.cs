using Characters.Interfaces;
using SystemLamplighter.Events;

namespace Characters.Interfaces;
	/// <summary>
	/// Interfaccia sull'esecuzione di un'azione
	/// </summary>
	public interface ICombatActionExecutor
	{
		void OnExecuteCombatAction (AtbExecuteActionEvent ev);
	}