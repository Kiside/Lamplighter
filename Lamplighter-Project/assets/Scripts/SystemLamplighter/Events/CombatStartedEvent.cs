using System.Collections.Generic;
using Characters.Interfaces;

namespace SystemLamplighter.Events
{
	public sealed class CombatStartedEvent
	{
		public readonly IReadOnlyList<ICombatActor> Actors;

		public CombatStartedEvent(IReadOnlyList<ICombatActor> actors)
		{
			Actors = actors;
		}
	}
}