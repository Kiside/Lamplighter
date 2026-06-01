using System;
using System.Collections.Generic;
using Godot;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.Debug;
using SystemLamplighter.Tool;

namespace SystemLamplighter.Extensions;

public static class NodeExstensions
{
	public static void CleanChildren(this Node node)
	{
		foreach(var child in node.GetChildren())
		{
			child.QueueFree();
		}
	}

	public static Identification SetIdentification(this Node node)
	{
		if (node.GetParent() is IIdentificable parent)
			return parent.Id;
		else
			Log.PrintWarning($"The parent of {node.Name} is not IIdentificable");

		return null;
	}

	public static Godot.Collections.Array<Node> GetNodesOfGroups(this Node node, params GroupsName[] groups)
	{
		if (groups == null || groups.Length == 0)
			return new Godot.Collections.Array<Node>();

		Godot.Collections.Array<Node> array = new Godot.Collections.Array<Node>();
		foreach (var g in groups)
			array.AddRange(node.GetTree().GetNodesInGroup($"{g}"));

		return array;
	}

	public static Godot.Collections.Array<Node> GetNodesOfGroups(this Node node, Godot.Collections.Array<GroupsName> groups)
	{
		Godot.Collections.Array<Node> array = new Godot.Collections.Array<Node>();
		foreach (var g in groups)
			array.AddRange(node.GetTree().GetNodesInGroup($"{g}"));

		return array;
	}

	// public static Godot.Collections.Array<Node> GetNodesOfGroups(this Node node, GroupsName group)
	// {
	// 	return node.GetTree().GetNodesInGroup($"{group}");
	// }



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