using System.Collections.Generic;
using System.Linq;
using Characters.Interfaces;
using SystemLamplighter.Debug;
using SystemLamplighter.Combat.Actor;
using SystemLamplighter.Interfaces;
using SystemLamplighter.Common.Enums;

namespace SystemLamplighter.Combat.Actor
{
	/// <summary>
	/// Classe per gestire CHI sta combattendo
	/// </summary>
	public class CombatActorRegistry : ICombatActorRegistry
	{
		private List<ICombatActor> _actors;

		public CombatActorRegistry()
		{
			_actors = new List<ICombatActor>();
		}
		
		public void Init(List<ICombatActor> actors) => _actors = actors;
		public void AddActor(ICombatActor actor) => _actors.Add(actor);
		public ICombatActor GetActor(int index) => _actors[index];
		public List<ICombatActor> GetActors() => _actors;
		public List<ICombatActor> GetActors(AtbCharacterType type) =>
		_actors.FindAll(a => a.AtbProperties.CharacterType == type);
		public ICombatActor GetActor(ICombatActor actor) => _actors.Find(a => a == actor);
		public int GetIndex(ICombatActor actor) => _actors.IndexOf(actor);
		public int Count => _actors.Count;

		public void RemoveActor(ICombatActor actor) => _actors.Remove(actor);
		public void Clear() => _actors.Clear();

	}
}