using System.Collections.Generic;
using Characters.Interfaces;

namespace SystemLamplighter
{
	public class CombatActorQuery : ICombatActorQuery
	{
		public List<ICombatActor> GetAllActors() => _actors;
		public List<ICombatActor> GetActors(AtbCharacterType type) =>
		_actors.FindAll(a => a.AtbProperties.CharacterType == type);
	}
}