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
	private Godot.Collections.Array<GroupsName> _groupsForWalkableElements = [GroupsName.walkable];
	[Export]
	private float _gridStep = 1.0f;
	[Export]
	private float _gridYTemp = 0.5f;

	private NavigationAstarService _navigationService;

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
		
		_navigationService.Init(this.GetNodesOfGroups(_groupsForWalkableElements), _gridStep, _gridYTemp);
	}

	public Godot.Vector3[] FindPath(Godot.Vector3 from, Godot.Vector3 to)
	{
		return _navigationService.FindPath(from, to);
	}
	
}