using System.IO;
using Godot;
using SystemLamplighter.Debug;
using SystemLamplighter.Tool;

[Tool]
public partial class DebugMasterNode : Node
{
	public T GetNodeOf<T>()
	{
		var root = GetParent();

		return Search<T>(root);
	}

	private T Search<T>(Node node)
	{
		if(node != null)
		{
			Log.PrintMessage($"searching: {node.Name} - TIPO: {node.GetScript()} -------- CERCO: {GetPath()}");
			if(node is T result)
				return result;
			foreach(var child in node.GetChildren())
			{
				var found = Search<T>(child);
				if(found != null)
					return found;
			}
		}
		

		return default(T);
	}
}