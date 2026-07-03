using System.Collections.Generic;
using Godot;

namespace SystemLamplighter.Navigation;


// TODO: OBSOLETO?
/// <summary>
/// Interfaccia per la navigazione Astar
/// </summary>
public interface INavigationAstar
{
	public Dictionary<Godot.Vector3, long> PointsDictionary {get;}
	public AStar3D Astar {get;}
	public void Init(Godot.Collections.Array<Node> walkables,float gridStep, float gridYTemp);
	public Godot.Vector3[] FindPath(Godot.Vector3 from, Godot.Vector3 to);
	public Godot.Vector3 FindClosestWaypointTo(Godot.Vector3 point);
}