using System.Collections.Generic;
using Characters.Interfaces;
using SystemLamplighter;
using SystemLamplighter.Tool;
using SystemLamplighter.Common.Enums;

namespace SystemLamplighter.Interfaces;

/// <summary>
/// Interfaccia per il registro dei personaggi che combattono
/// </summary>
public interface ICombatActorProvider
{
	public void Init(List<ICombatActor> actors);
	public void RemoveActor(ICombatActor actor);
	public void AddActor(ICombatActor actor);
	public void Clear();
	public List<ICombatActor> GetActors();
	public List<ICombatActor> GetActors(AtbCharacterType type);
	public ICombatActor GetActor(int index);
	public ICombatActor GetActor(ICombatActor actor);
	public int GetIndex(ICombatActor actor);
	public int Count {get;}
}