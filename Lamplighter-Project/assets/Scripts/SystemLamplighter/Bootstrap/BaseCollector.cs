using Godot;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.Debug;

namespace SystemLamplighter.Bootstrap
{
	/// <summary>
	/// Classe generica per i "Collector" oggetti che si occupano di collezionare i nodi di uno stesso gruppo
	/// </summary>
	public partial class BaseCollector : Node
	{
		[Export]
		protected  Godot.Collections.Array<GroupsName> _groups;

		protected Godot.Collections.Array<Node> GetNodesOfGroups()
		{
			DebugLamplighter.Assert(_groups != null, "_groups is null");

			if(_groups == null && _groups.Count <= 0)
				return null;

			Godot.Collections.Array<Node> array = new Godot.Collections.Array<Node>();
			foreach (var g in _groups)
			{
				array.AddRange(GetTree().GetNodesInGroup($"{g}"));
			}

			return array;

		}
	}
}