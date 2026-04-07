using System;
using System.Collections.Generic;
using System.Numerics;
using Godot;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.Debug;
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

	private AStar3D _astar;
	public AStar3D Astar => _astar;
	private Dictionary<Godot.Vector3, long> _pointsDictionary;
	public Dictionary<Godot.Vector3, long> PointsDictionary => _pointsDictionary;


	private NavigationAstarService _navigationService;

	// TODO: il calcolo dei point è dove metterli, probabilmente può essere una classe così da lasciare il lavoro ad altri e renderlo modulabile

	public override void _Ready()
	{
		base._Ready();
		Init();
	}

	public void BootstrapInit(NavigationAstarService navigationAstarService)
	{
		_navigationService = navigationAstarService;
	}

	public void Init()
	{
		DebugLamplighter.Assert(_navigationService != null, "Il navigation system service è null ma non dovrebbe");
		
		_navigationService.Init(this.GetNodesOfGroups(_groups), _gridStep, _gridYTemp);
	}

	public Godot.Vector3[] FindPath(Godot.Vector3 from, Godot.Vector3 to)
	{
		return _navigationService.FindPath(from, to);
	}
	
}