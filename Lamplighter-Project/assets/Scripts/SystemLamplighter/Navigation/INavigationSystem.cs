using System.Collections.Generic;
using Godot;

public interface INavigationSystem
{
	public Godot.Vector3[] FindPath(Godot.Vector3 from, Godot.Vector3 to);
}