using System.Collections.Generic;
using Characters.Interfaces;
using SystemLamplighter;

public interface ICombatActorRegistry
{
	public void Init(List<ICombatActor> actors);
	public void RemoveActor(ICombatActor actor);
	public void AddActor(ICombatActor actor);
	public void Clear();
	public List<ICombatActor> GetAllActors();
	public List<ICombatActor> GetActors(AtbCharacterType type);
}