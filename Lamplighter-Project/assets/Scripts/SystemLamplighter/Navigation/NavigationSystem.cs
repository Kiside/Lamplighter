using System.Numerics;
using Godot;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.Extensions;

public partial class NavigationSystem : Node
{
	[Export]
	private Godot.Collections.Array<GroupsName> _groups = [GroupsName.walkable];

	private AStar3D _astar;

	public override void _Ready()
	{
		base._Ready();

		var walkables = this.GetNodesOfGroups(_groups);
	}

	private void Init()
	{
		var walkables = this.GetNodesOfGroups(_groups);
		

		foreach (var walkable in walkables)
		{
			Godot.Vector3 origin = new Godot.Vector3();
			if(walkable is MeshInstance3D mesh)
				origin = GetOriginPosition(mesh);
			if (walkable.GetChildCount() > 0)
			{
				foreach(var wChild in walkable.GetChildren())
				{
					if(wChild is MeshInstance3D childMesh)
						origin = GetOriginPosition(childMesh);	
				}
			}
			
		}
	}

	private Godot.Vector3 GetOriginPosition(MeshInstance3D mesh)
	{
		Aabb aabb = mesh.GetAabb();
		return aabb.Position;
	}
	
}