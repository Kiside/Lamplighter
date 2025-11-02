using Godot;

[Tool]
public partial class DebugButton : Button
{
	public override void _Process(double delta)
	{
		if (!Engine.IsEditorHint())
			return;

		if (Text != Name)
			Text = Name;
	}
}