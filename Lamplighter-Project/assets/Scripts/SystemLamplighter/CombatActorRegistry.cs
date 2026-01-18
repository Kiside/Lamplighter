using System.Collections.Generic;
using System.Linq;
using Characters.Interfaces;
using SystemLamplighter.Debug;

namespace SystemLamplighter
{
	public class CombatActorRegistry : ICombatActorRegistry
	{
		private List<ICombatActor> _actors;

		public CombatActorRegistry()
		{
			_actors = new List<ICombatActor>();
		}
		
		public void Init(List<ICombatActor> actors) => _actors = actors;
		public void AddActor(ICombatActor actor) => _actors.Add(actor);
		public List<ICombatActor> GetAllActors()
		{
			

			return _actors;
		}
		public List<ICombatActor> GetActors(AtbCharacterType type) =>
		_actors.FindAll(a => a.AtbProperties.CharacterType == type);

		public void RemoveActor(ICombatActor actor) => _actors.Remove(actor);
		public void Clear() => _actors.Clear();

	}
}