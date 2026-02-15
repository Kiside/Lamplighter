using System.Collections.Generic;
using Characters.Interfaces;
using Godot;

public interface ICombatActorPositionProvider<TNodeType>
{
	public void Init(Dictionary<ICombatActor, TNodeType> actors);
	Vector3 GetPosition(ICombatActor actor);
}