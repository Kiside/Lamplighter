using Characters.Interfaces;
namespace Characters.Inteaces
{
	public interface ICombatActionExecutor
	{
		void OnExecuteCombatAction (AtbCommandPhaseStartedEvent ev);
	}
}