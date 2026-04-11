using System.Collections.Generic;
using Godot;

public class NavigationAstarService : INavigationAstar
{
	// ? Ma va bene avere un unico dizionario per tutti i punti anche se quest'ultimi possono essere su mesh diverse?
	public float GridStep;
	public float GridYTemp;
	public Dictionary<Godot.Vector3, long> PointsDictionary { get; private set; }
	public AStar3D Astar { get; private set; }

	public NavigationAstarService()
	{
		PointsDictionary = new Dictionary<Godot.Vector3, long>();
		Astar = new AStar3D();
		GridStep = 0;
		GridYTemp = 0;
	}

	public void Init(Godot.Collections.Array<Node> walkables, float gridStep, float gridYTemp)
	{
		GridStep = gridStep;
		GridYTemp = gridYTemp; 

		if(PointsDictionary == null)
			PointsDictionary = new Dictionary<Godot.Vector3, long>();
		else if(PointsDictionary.Count > 0)
		 	PointsDictionary.Clear();

		if(Astar == null)
        	Astar = new AStar3D();
    	else
        	Astar.Clear();


		GD.Print($"Walkables trovati: {walkables.Count}"); // <- quanti ne trova?
		GetWalkables(walkables);
	}

	public Godot.Vector3[] FindPath(Godot.Vector3 from, Godot.Vector3 to)
	{
		var fromId = Astar.GetClosestPoint(from);
		var toId = Astar.GetClosestPoint(to);
		return Astar.GetPointPath(fromId,toId);
	}

	public Godot.Vector3 FindClosestWaypointTo(Godot.Vector3 point)
	{
		var closestId = Astar.GetClosestPoint(point);
		return Astar.GetPointPosition(closestId);
	}

	private void CalculatePoints(MeshInstance3D mesh, Vector3 origin)
	{
		Aabb aabb = mesh.GetAabb();
		origin = aabb.Position;
		var x_steps = aabb.Size.X / GridStep;
		var z_steps = aabb.Size.Z / GridStep;
		var surfaceY = aabb.Position.Y + aabb.Size.Y;

		for (var x = 0; x < x_steps; x++)
		{
			for (var z = 0; z < z_steps; z++)
			{
				var nextPoint = origin + new Godot.Vector3(x * GridStep, 0, z * GridStep);
				nextPoint.Y = surfaceY + 0.05f;
				AddPoint(nextPoint);
			}
		}
	}

	private void GetWalkables(Godot.Collections.Array<Node> walkables)
	{
		foreach (var walkable in walkables)
		{
			if (walkable is MeshInstance3D mesh)
			{
				CalculatePoints(mesh, new Godot.Vector3());
			}
			if (walkable.GetChildCount() > 0)
			{
				foreach (var wChild in walkable.GetChildren())
				{
					if (wChild is MeshInstance3D childMesh)
					{
						CalculatePoints(childMesh, new Vector3());
					}

				}
			}

			ConnectPoints();
		}
	}

	private void ConnectPoints()
	{
		foreach (var point in PointsDictionary.Keys)
		{
			for (var x = -GridStep; x <= GridStep; x++)
			{
				var currentId = PointsDictionary[point];
				for (var z = -GridStep; z <= GridStep; z++)
				{
					var searchOffset = new Godot.Vector3(x, 0, z);
					if (searchOffset != Godot.Vector3.Zero)
					{
						var potentialNeighbor = WorldToAStar(point + searchOffset);
						if (PointsDictionary.ContainsKey(potentialNeighbor))
						{
							var neighborId = PointsDictionary[potentialNeighbor];
							if (!Astar.ArePointsConnected(currentId, neighborId))
								Astar.ConnectPoints(currentId, neighborId);
						}
					}
				}
			}
		}
	}

	private void AddPoint(Godot.Vector3 point)
	{
		//point.Y = _gridYTemp;

		var id = Astar.GetAvailablePointId();
		Astar.AddPoint(id, point);
		PointsDictionary.TryAdd(WorldToAStar(point), id);
	}

	private Godot.Vector3 WorldToAStar(Godot.Vector3 worldPoint)
	{
		return worldPoint.Snapped(GridStep);
	}

	public void Dispose()
	{
		Astar.Clear();
		PointsDictionary.Clear();
	}
}