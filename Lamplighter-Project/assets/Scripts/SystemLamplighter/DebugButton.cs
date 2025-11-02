using Godot;

[Tool]
public partial class DebugButton : Button
{
	public override void _EnterTree()
	{
		//base._EnterTree();

		this.Text = Name;
	}


}