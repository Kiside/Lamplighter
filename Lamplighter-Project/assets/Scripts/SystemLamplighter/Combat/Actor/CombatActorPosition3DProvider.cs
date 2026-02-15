using System.Collections.Generic;
using Characters.Interfaces;
using Godot;
using SystemLamplighter.Debug;

public class CombatActorPosition3DProvider : ICombatActorPositionProvider<Node3D>
{
	private Dictionary<ICombatActor, Node3D> _actorPositions;

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
}