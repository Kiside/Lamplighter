using System.Collections.Generic;
using Characters.Interfaces;
using SystemLamplighter;

public interface ICombatActorRegistry
{
	public void Init(List<ICombatActor> actors);
	public void RemoveActor(ICombatActor actor);
	public void AddActor(ICombatActor actor);
	public void Clear();
	public List<ICombatActor> GetActors();
	public List<ICombatActor> GetActors(AtbCharacterType type);
	public ICombatActor GetActor(int index);
}