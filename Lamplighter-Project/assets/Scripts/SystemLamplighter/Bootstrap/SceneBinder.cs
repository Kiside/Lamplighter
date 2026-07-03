using System.Collections;
using System.Linq;
using Godot;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.Debug;
using SystemLamplighter.Extensions;

namespace SystemLamplighter.Bootstrap;

/// <summary>
/// Classe che si occupa di Bindare in modo automatico i nodi
/// </summary>
public partial class SceneBinder : Node
{
	[Export]
	private Godot.Collections.Array<GroupsName> _getNodesOfGroups;

	public System.Collections.Generic.IEnumerable<T> BindAll<T>() where T : Node
	{
		var nodes = this.GetNodesOfGroups(_getNodesOfGroups);
		return nodes.OfType<T>();
	}

	public T Bind<T>() where T : Node
	{
		DebugLamplighter.Assert(_getNodesOfGroups != null && _getNodesOfGroups.Count > 0, "Impossibile continuare, _getNodesOfGroups non può essere vuoto");

		var nodes = this.GetNodesOfGroups(_getNodesOfGroups);
    	return nodes.FirstOrDefault(n => n is T) as T;
	}
}