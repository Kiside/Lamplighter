using System.Collections.Generic;
using Characters.Interfaces;

namespace SystemLamplighter.Events
{
	/// <summary>
	/// Evento di inizio combattimento
	/// </summary>
	public sealed class CombatStartedEvent
	{
		public readonly IReadOnlyList<ICombatActor> Actors;

		public CombatStartedEvent(IReadOnlyList<ICombatActor> actors)
		{
			Actors = actors;
		}
	}
}