using System.Collections.Generic;
using Characters.Interfaces;
using Godot;

namespace SystemLamplighter.Interfaces;

// TODO: Probabilmente da cancellare
public interface ICombatActorPositionProvider<TNodeType>
{
	public void Init(Dictionary<ICombatActor, TNodeType> actors);
	public Vector3 GetPosition(ICombatActor actor);
	public List<Vector3> GetPositionsFromActors(List<ICombatActor> actors);
	public ICombatActor GetFirstActor();
	public List<ICombatActor> GetActors();
	public ICombatActor GetActor(int index);
	public int GetIndex(ICombatActor actor);
	public int Count { get; }
}