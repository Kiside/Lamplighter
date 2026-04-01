using System;
using System.Collections.Generic;
using System.Numerics;
using Godot;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.Extensions;
using SystemLamplighter.Tool;

namespace SystemLamplighter.Navigation;

//[Tool]
public partial class NavigationSystem : Node, INavigationSystem
{
	[Export]
	private Godot.Collections.Array<GroupsName> _groups = [GroupsName.walkable];
	[Export]
	private float _gridStep = 1.0f;
	[Export]
	private float _gridYTemp = 0.5f;

	// [ExportToolButton("BakePoints")]
	// public Callable BakePointsButton => Callable.From(BakeButtonClick);
	// [ExportToolButton("ClearPoints")]
	// public Callable ClearPointsButton => Callable.From(ClearButtonClick);
	// [Export]
	// public float _debugPointSize = 5f;
	// [Export]
	// public Color _debugColorPoint;

	private AStar3D _astar;
	public AStar3D Astar => _astar;
	private Dictionary<Godot.Vector3, long> _pointsDictionary;
	public Dictionary<Godot.Vector3, long> PointsDictionary => _pointsDictionary;

	// TODO: il calcolo dei point è dove metterli, probabilmente può essere una classe così da lasciare il lavoro ad altri e renderlo modulabile

	public override void _Ready()
	{
		//if(Engine.IsEditorHint()) return; // in editor _Ready non fa nulla
		base._Ready();
		Init();
	}

	public void Init()
	{
		if(_pointsDictionary == null)
			_pointsDictionary = new Dictionary<Godot.Vector3, long>();
		else 
		 	_pointsDictionary.Clear();

		if(_astar == null)
        	_astar = new AStar3D();
    	else
        	_astar.Clear();

		var walkables = this.GetNodesOfGroups(_groups);
		GD.Print($"Walkables trovati: {walkables.Count}"); // <- quanti ne trova?
		
		GetWalkables(walkables);
		
		GD.Print($"Points aggiunti: {_pointsDictionary.Count}"); // <- quanti punti?
	}
	
	private void GetWalkables(Godot.Collections.Array<Node> walkables)
	{
		foreach (var walkable in walkables)
		{
			
			Godot.Vector3 origin = new Godot.Vector3();

			if(walkable is MeshInstance3D mesh)
			{
						Aabb aabb = mesh.GetAabb();
						GD.Print($"AABB : {aabb}");
						origin = aabb.Position;
						var x_steps = aabb.Size.X / _gridStep;
						var z_steps = aabb.Size.Z / _gridStep;
						var surfaceY = aabb.Position.Y + aabb.Size.Y;


						for(var x = 0; x < x_steps ; x++)
						{
							for(var z = 0; z < z_steps ; z++)
							{
								var nextPoint = origin + new Godot.Vector3(x * _gridStep, 0, z * _gridStep);
								nextPoint.Y = surfaceY + 0.05f;
								AddPoint(nextPoint);
								
							}
						}
			}
			if (walkable.GetChildCount() > 0)
			{
				foreach(var wChild in walkable.GetChildren())
				{
					if(wChild is MeshInstance3D childMesh)
					{
						Aabb aabb = childMesh.GetAabb();
						GD.Print($"AABB : {aabb}");
						origin = aabb.Position;
						var x_steps = aabb.Size.X / _gridStep;
						var z_steps = aabb.Size.Z / _gridStep;
						var surfaceY = aabb.Position.Y + aabb.Size.Y;


						for(var x = 0; x < x_steps ; x++)
						{
							for(var z = 0; z < z_steps ; z++)
							{
								var nextPoint = origin + new Godot.Vector3(x * _gridStep, 0, z * _gridStep);
								nextPoint.Y = surfaceY + 0.05f;
								AddPoint(nextPoint);
								
							}
						}
					}
							
				}
			}
			
		}
	}

	private void ConnectPoints()
	{
		foreach(var point in _pointsDictionary.Keys)
		{
			for(var x = -_gridStep ; x <= _gridStep ; x++)
			{
				var currentId = _pointsDictionary[point];
				for(var z = -_gridStep ; z<= _gridStep ; z++ )
				{
					var searchOffset = new Godot.Vector3(x,0,z);
					if(searchOffset != Godot.Vector3.Zero)
					{
						var potentialNeighbor = WorldToAStar(point + searchOffset);
						if(_pointsDictionary.ContainsKey(potentialNeighbor))
						{
							var neighborId = _pointsDictionary[potentialNeighbor];
							if(!_astar.ArePointsConnected(currentId, neighborId))
								_astar.ConnectPoints(currentId, neighborId);
						}
					}
				}
			}
		}
	}

	private Godot.Vector3[] FindPath(Godot.Vector3 from, Godot.Vector3 to)
	{
		var fromId = _astar.GetClosestPoint(from);
		var toId = _astar.GetClosestPoint(to);
		return _astar.GetPointPath(fromId,toId);
	}

	private void AddPoint(Godot.Vector3 point)
	{
		//point.Y = _gridYTemp;

		var id = _astar.GetAvailablePointId();
		_astar.AddPoint(id, point);
		_pointsDictionary.Add(WorldToAStar(point), id);
	}

	private Godot.Vector3 WorldToAStar(Godot.Vector3 worldPoint)
	{
		return worldPoint.Snapped(_gridStep);
	}

	private Godot.Vector3 GetOriginPosition(MeshInstance3D mesh)
	{
		Aabb aabb = mesh.GetAabb();
		return aabb.Position;
	}

	// #region DEBUG
	// private ImmediateMesh _debugMesh;
	// private MeshInstance3D _debugMeshInstance;

	// private void BakeButtonClick()
	// {
	// 		if(Engine.IsEditorHint())
	// 		{
	// 			ClearDebugPoints();
	// 			Init();
	// 			DrawDebugPoints();
	// 			NotifyPropertyListChanged();
	// 		}
	// }

	// private void ClearButtonClick()
	// {
	// 	if(Engine.IsEditorHint())
	// 		{
	// 			ClearDebugPoints();
	// 			NotifyPropertyListChanged();
	// 		}
	// }

	// private void DrawDebugPoints()
	// {
	// 	if(_debugMeshInstance == null)
	// 	{
	// 		_debugMeshInstance = new MeshInstance3D();
	// 		_debugMesh = new ImmediateMesh();
	// 		_debugMeshInstance.Mesh = _debugMesh;

	// 		// Aggiungi il materiale
	// 		var material = new StandardMaterial3D();
	// 		material.AlbedoColor = _debugColorPoint;
	// 		material.ShadingMode = BaseMaterial3D.ShadingModeEnum.Unshaded;
	// 		material.VertexColorUseAsAlbedo = true; // usa il colore del vertice
	// 		material.UsePointSize = true;
	// 		material.PointSize = _debugPointSize;
	// 		_debugMeshInstance.MaterialOverride = material;

	// 		AddChild(_debugMeshInstance);
	// 	}

	// 	_debugMesh.ClearSurfaces();
	// 	_debugMesh.SurfaceBegin(Mesh.PrimitiveType.Points);

	// 	foreach(var point in _pointsDictionary.Keys)
	// 	{
	// 		_debugMesh.SurfaceSetColor(Colors.Green);
	// 		_debugMesh.SurfaceAddVertex(point);
	// 	}

	// 	_debugMesh.SurfaceEnd();
	// }
	// private void ClearDebugPoints()
	// {
	// 	Log.PrintMessage("qui1");
	// 	if(_debugMeshInstance != null)
	// 	{
	// 		Log.PrintMessage("qui2");
	// 		_debugMeshInstance.QueueFree();
	// 		_debugMeshInstance = null;
	// 		_debugMesh = null;
	// 	}
		
	// 	if(_pointsDictionary != null)
	// 		_pointsDictionary.Clear();

	// 	if(_astar != null)
	// 		_astar.Clear();
	// }
	// #endregion
	
}