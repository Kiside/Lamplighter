using System;
using System.Collections.Generic;
using Godot;
using SystemLamplighter.Common.Enums;

namespace SystemLamplighter.Extensions;

public static class NodeExstensions
{
	public static Godot.Collections.Array<Node> GetNodesOfGroups(this Node node, Godot.Collections.Array<GroupsName> _groups)
	{
		if(_groups == null && _groups.Count <= 0)
				return null;

			Godot.Collections.Array<Node> array = new Godot.Collections.Array<Node>();
			foreach (var g in _groups)
			{
				array.AddRange(node.GetTree().GetNodesInGroup($"{g}"));
			}

			return array;
	}

	public static Godot.Collections.Array<Node> GetNodesOfGroup(this Node node, GroupsName group)
	{
		return node.GetTree().GetNodesInGroup($"{group}");
	}


	public static void AddToGroups(this Node node, List<string> groups)
	{
		foreach (var group in groups)
		{
			node.AddToGroup(group);
		}
	}

	public static void RemoveFromGroups(this Node node, List<string> groups)
	{
		foreach (var group in groups)
		{
			node.RemoveFromGroup(group);
		}
	}
}