using Characters.Interfaces;

namespace SystemLamplighter.Events
{
	/// <summary>
	/// Classe per l'evento di fine esecuzione azione
	/// </summary>
	public sealed class AtbEndExecuteActionEvent
	{
		public ICombatActor Actor;

		public AtbEndExecuteActionEvent(ICombatActor actor) {Actor = actor;}
	}
}