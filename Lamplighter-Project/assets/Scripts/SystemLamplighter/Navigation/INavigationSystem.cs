using System.Collections.Generic;
using Godot;

public interface INavigationSystem
{
	public Dictionary<Godot.Vector3, long> PointsDictionary {get;}
	public AStar3D Astar {get;}
	public void Init();
}