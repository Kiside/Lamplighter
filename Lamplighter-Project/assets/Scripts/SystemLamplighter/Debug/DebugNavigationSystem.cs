using System.Collections.Generic;
using System.Diagnostics;
using Godot;
using Microsoft.Extensions.DependencyInjection;
using SystemLamplighter.Bootstrap;
using SystemLamplighter.Debug;
using SystemLamplighter.Extensions;
using SystemLamplighter.Navigation;
using SystemLamplighter.Tool;

[Tool]
public partial class DebugNavigationSystem : Node
{
	
	[Export]
	private float _gridStep = 1.0f;
	[Export]
	private float _gridYTemp = 0.5f;

	[ExportToolButton("BakePoints")]
	public Callable BakePointsButton => Callable.From(BakeButtonClick);
	[ExportToolButton("ClearPoints")]
	public Callable ClearPointsButton => Callable.From(ClearButtonClick);
	[Export]
	public float _debugPointSize = 5f;
	[Export]
	public Color _debugColorPoint;

	private ImmediateMesh _debugMesh;
	private MeshInstance3D _debugMeshInstance;
	//private NavigationSystem _navigationSystem;

	private NavigationAstarService _navigationService;

	public override void _EnterTree()
	{
		if(Engine.IsEditorHint()) return;
		
		base._EnterTree();
	}

	private void InitNavigationService()
	{
		if(_navigationService == null)
					_navigationService = new NavigationAstarService();
	}


	// public void Init()
	// {
	// 	if(_pointsDictionary == null)
	// 		_pointsDictionary = new Dictionary<Godot.Vector3, long>();
	// 	else 
	// 	 	_pointsDictionary.Clear();

	// 	if(_astar == null)
    //     	_astar = new AStar3D();
    // 	else
    //     	_astar.Clear();

	// 	var walkables = this.GetNodesOfGroups(_groups);
	// 	GD.Print($"Walkables trovati: {walkables.Count}"); // <- quanti ne trova?
		
	// 	GetWalkables(walkables);
		
	// 	GD.Print($"Points aggiunti: {_pointsDictionary.Count}"); // <- quanti punti?
	// }

	// private bool GetNavigationSystem()
	// {
	// 	if(this.GetParent() is DebugMasterNode debugMasterNode)
	// 	{
	// 		if(_navigationService == null)
	// 			_navigationService = debugMasterNode.GetNodeOf<NavigationSystem>();
	// 		if(_navigationService != null)
	// 		{
	// 			Log.PrintMessage("perfect");
	// 			return true;
	// 		}
	// 	}
	// 	Log.PrintMessage("esco");
	// 	return false;
	// }

	private void BakeButtonClick()
	{
			if(Engine.IsEditorHint())
			{
			// 	if(!GetNavigationSystem())
			// {
			// 	DebugLamplighter.Assert(true, "Can't get navigtationsystem");
			// 	return;
			// }

				// _navigationSystem = GetParent() as NavigationSystem;
				// _navigationSystem.Init();
				//ClearDebugPoints();
				InitNavigationService();
				_navigationService.Init(this.GetNodesOfGroup(SystemLamplighter.Common.Enums.GroupsName.walkable), _gridStep, _gridYTemp);
				DrawDebugPoints();
				NotifyPropertyListChanged();
			}
	}

	private void ClearButtonClick()
	{
		if(Engine.IsEditorHint())
			{
				// if(!GetNavigationSystem())
				// {
				// 	DebugLamplighter.Assert(true, "Can't get navigtationsystem");
				// 	return;
				// }
				//_navigationSystem = GetParent() as NavigationSystem;
				InitNavigationService();
				ClearDebugPoints();
				NotifyPropertyListChanged();
			}
	}

	private void DrawDebugPoints()
	{
		if(_debugMeshInstance == null)
		{
			_debugMeshInstance = new MeshInstance3D();
			_debugMesh = new ImmediateMesh();
			_debugMeshInstance.Mesh = _debugMesh;

			// Aggiungi il materiale
			var material = new StandardMaterial3D();
			material.AlbedoColor = _debugColorPoint;
			material.ShadingMode = BaseMaterial3D.ShadingModeEnum.Unshaded;
			material.VertexColorUseAsAlbedo = true; // usa il colore del vertice
			material.UsePointSize = true;
			material.PointSize = _debugPointSize;
			_debugMeshInstance.MaterialOverride = material;

			AddChild(_debugMeshInstance);
		}

		_debugMesh.ClearSurfaces();
		_debugMesh.SurfaceBegin(Mesh.PrimitiveType.Points);

		foreach(var point in _navigationService.PointsDictionary.Keys)
		{
			_debugMesh.SurfaceSetColor(Colors.Green);
			_debugMesh.SurfaceAddVertex(point);
		}

		_debugMesh.SurfaceEnd();
	}
	private void ClearDebugPoints()
	{
		Log.PrintMessage("qui1");
		if(_debugMeshInstance != null)
		{
			Log.PrintMessage("qui2");
			_debugMeshInstance.QueueFree();
			_debugMeshInstance = null;
			_debugMesh = null;
		}
		
		if(_navigationService.PointsDictionary != null)
			_navigationService.PointsDictionary.Clear();

		if(_navigationService.Astar != null)
			_navigationService.Astar.Clear();
	}

	public override void _ExitTree()
	{
		if(_navigationService != null)
		{
			ClearButtonClick();
			_navigationService.Dispose();
			_navigationService = null;
		}
			
		base._ExitTree();
	}
}