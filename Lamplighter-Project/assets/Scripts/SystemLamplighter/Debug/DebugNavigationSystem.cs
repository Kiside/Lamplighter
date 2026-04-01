using System.Collections.Generic;
using System.Diagnostics;
using Godot;
using SystemLamplighter.Debug;
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
	private INavigationSystem _navigationSystem;

	public override void _EnterTree()
	{
		if(Engine.IsEditorHint()) return;
		
		base._EnterTree();
	}

	private bool GetNavigationSystem()
	{
		if(this.GetParent() is DebugMasterNode debugMasterNode)
		{
			_navigationSystem = debugMasterNode.GetNodeOf<INavigationSystem>();
			if(_navigationSystem != null)
			{
				Log.PrintMessage("perfect");
				return true;
			}
		}
		Log.PrintMessage("esco");
		return false;
	}

	private void BakeButtonClick()
	{
			if(Engine.IsEditorHint())
			{
				if(!GetNavigationSystem())
			{
				DebugLamplighter.Assert(true, "Can't get navigtationsystem");
				return;
			}
				_navigationSystem.Init();
				ClearDebugPoints();
				DrawDebugPoints();
				NotifyPropertyListChanged();
			}
	}

	private void ClearButtonClick()
	{
		if(Engine.IsEditorHint())
			{
				if(!GetNavigationSystem())
				{
					DebugLamplighter.Assert(true, "Can't get navigtationsystem");
					return;
				}
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

		foreach(var point in _navigationSystem.PointsDictionary.Keys)
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
		
		if(_navigationSystem.PointsDictionary != null)
			_navigationSystem.PointsDictionary.Clear();

		if(_navigationSystem.Astar != null)
			_navigationSystem.Astar.Clear();
	}
}