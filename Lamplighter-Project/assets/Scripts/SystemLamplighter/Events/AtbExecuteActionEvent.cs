using Characters.Interfaces;

namespace SystemLamplighter.Events
{
	/// <summary>
	/// Classe per l'evento di esecuzione azione
	/// </summary>
	public sealed class AtbExecuteActionEvent
	{
		public ICombatActor Actor {get;}
		public AtbExecuteActionEvent(ICombatActor actor)
		{
			Actor = actor;
		}
	}
}