using Characters.Interfaces;

namespace SystemLamplighter.Events
{
	public sealed class AtbExecuteActionEvent
	{
		public ICombatActor Actor {get;}
		public AtbExecuteActionEvent(ICombatActor actor)
		{
			Actor = actor;
		}
	}
}