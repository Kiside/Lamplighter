using Godot;

[Tool]
public partial class DebugButton : Button
{
	[ExportToolButton("EditNameNode")]
	public Callable EditNameNodeButton => Callable.From(EditNameNodeButtonFunc);

	public void EditNameNodeButtonFunc()
	{
		Name = Text;
	}
}