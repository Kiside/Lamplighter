using Godot;
using SystemLamplighter;

public partial class GameManager : Node
{
	public override void _EnterTree()
	{
		base._EnterTree();
	}

	public override void _ExitTree()
	{
		Log.Dispose();
	}
}