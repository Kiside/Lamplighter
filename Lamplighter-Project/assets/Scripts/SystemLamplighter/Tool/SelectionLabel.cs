using Godot;
using System;

[Tool]
public partial class SelectionLabel : HBoxContainer
{
	public Label Label {get; private set;}
	public TextureRect TextureRect {get; private set;}

	private MarginContainer _textureRectContainer;
	private MarginContainer _labelContainer;

	public override void _EnterTree()
	{
		Label = new Label();
		TextureRect = new TextureRect();

		_textureRectContainer = new MarginContainer();
		_labelContainer = new MarginContainer();

		AddChild(_textureRectContainer);
		AddChild(_labelContainer);

		_textureRectContainer.AddChild(TextureRect);
		_labelContainer.AddChild(Label);

		_textureRectContainer.AddThemeConstantOverride("margin_top", 10);
		_textureRectContainer.AddThemeConstantOverride("margin_left", 2);
		_textureRectContainer.AddThemeConstantOverride("margin_bottom", 10);
		_textureRectContainer.AddThemeConstantOverride("margin_right", 2);

		_labelContainer.AddThemeConstantOverride("margin_top", 5);
		_labelContainer.AddThemeConstantOverride("margin_bottom", 5);

		Label.Text = "Test";
		base._EnterTree();
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
