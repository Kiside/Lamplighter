using System.Collections.Generic;
using Godot;
using SystemLamplighter.Tool;
using SystemLamplighter.Interfaces;

namespace SystemLamplighter.Setup;
/// <summary>
/// Classe per aggiungere il nodo in un gruppo in modo automatico
/// </summary>
public class GroupsInitiator : IGroupsInitiator
{
	List<string> _groups;

	public GroupsInitiator(List<string> groups, Node node)
	{
		_groups = groups;
		GroupsInit(node);
	}

	public void GroupsInit(Node node)
	{
		if (_groups == null || _groups.Count <= 0)
		{
			Log.PrintMessage("There are no groups");
			return;
		}

		foreach (var group in _groups)
		{
			node.AddToGroup(group);

		}
	}

	public void RemoveFromGroups(Node node)
	{
		foreach (var group in _groups)
		{
			node.RemoveFromGroup(group);
		}
	}
}