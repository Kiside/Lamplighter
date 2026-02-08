
using Characters.Interfaces;

namespace SystemLamplighter.Events
{
	/// <summary>
	/// Classe per l'evento di fine fase di comando
	/// </summary>
	public sealed class AtbCommandPhaseEndEvent
	{
		public ICombatActor Actor {get;}

		public AtbCommandPhaseEndEvent(ICombatActor actor)
		{
			Actor = actor;
		}

	}
}