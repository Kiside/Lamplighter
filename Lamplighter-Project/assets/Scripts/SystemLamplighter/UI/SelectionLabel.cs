using Godot;
using System;

namespace LamplighterPlugins.CustomNodes;

public partial class SelectionLabel : HBoxContainer
{
	[Export]
	public TextureRect Texture {get; private set;}
	[Export]
	public Label Label {get; private set;}

	public bool IsSelected => Texture.Visible;

	public void SetLabelText(string text) => Label.Text = text;

	public void Focus() 
	{
		Label.AddThemeColorOverride("font_color", new Color(0.0f, 1.0f, 0.8f));
	}

	public void Unfocus()
	{
		Label.AddThemeColorOverride("font_color", new Color(1f, 1f, 1f));
	}

    public void Select() => Texture.Visible = true;

	public void Deselect() => Texture.Visible = false;
}