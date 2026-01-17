using System.Collections.Generic;
using Characters.Interfaces;
using SystemLamplighter;

public interface ICombatActorQuery
{
	public List<ICombatActor> GetAllActors();
	public List<ICombatActor> GetActors(AtbCharacterType type);
}