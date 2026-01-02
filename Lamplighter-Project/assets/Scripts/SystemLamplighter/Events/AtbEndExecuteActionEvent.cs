using Characters.Interfaces;

namespace SystemLamplighter.Events
{
	public sealed class AtbEndExecuteActionEvent
	{
		public ICombatActor Actor;

		public AtbEndExecuteActionEvent(ICombatActor actor) {Actor = actor;}
	}
}