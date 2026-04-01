using Godot;
using SystemLamplighter.Tool;

[Tool]
public partial class DebugMasterNode : Node
{
	public T GetNodeOf<T>()
	{
		var root = GetTree().Root;

		return Search<T>(root);
	}

	private T Search<T>(Node node)
	{
		Log.PrintMessage($"searching: {node.Name} - tipo: {node.GetType().Name} - cerco: {typeof(T).Name}");
		if(node is T result)
			return result;
		foreach(var child in node.GetChildren())
		{
			var found = Search<T>(child);
			if(found != null)
				return found;
		}

		return default(T);
	}
}