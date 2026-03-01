using System.Collections.Generic;
using System.Linq;
using Characters.Interfaces;
using Godot;
using SystemLamplighter.Debug;


namespace SystemLamplighter.Combat.Actor;

/// <summary>
/// Classe per gestire le posizioni dei personaggi in battaglia
/// </summary>
public class CombatActorPosition3DProvider : ICombatActorPositionProvider<Node3D>
{
	private Dictionary<ICombatActor, Node3D> _actorPositions;

	public int Count => _actorPositions.Count;

	public CombatActorPosition3DProvider()
	{
		_actorPositions = new Dictionary<ICombatActor, Node3D>();
	}

	public void Init(Dictionary<ICombatActor, Node3D> actors)
	{
		DebugLamplighter.Assert(actors != null, "actors is null");
		
		if(_actorPositions != null && _actorPositions.Count > 0)
			_actorPositions.Clear();

		_actorPositions = actors;
	}

	public Vector3 GetPosition(ICombatActor actor) => _actorPositions[actor].GlobalPosition;
	public List<Vector3> GetPositionsFromActors(List<ICombatActor> actors)
	{
		List<Vector3> positions = new List<Vector3>();
		foreach(var a in actors)
			positions.Add(_actorPositions[a].GlobalPosition);
		return positions;
	}
	public ICombatActor GetActor(int index) => _actorPositions.Keys.ToList()[index];
	public List<ICombatActor> GetActors() => _actorPositions.Keys.ToList();
	public ICombatActor GetFirstActor() => _actorPositions.First().Key;
}