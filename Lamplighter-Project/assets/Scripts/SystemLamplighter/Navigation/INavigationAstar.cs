using System.Collections.Generic;
using Godot;
public interface INavigationAstar
{
	public Dictionary<Godot.Vector3, long> PointsDictionary {get;}
	public AStar3D Astar {get;}
	public void Init(Godot.Collections.Array<Node> walkables,float gridStep, float gridYTemp);
}