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


    public void Select() => Texture.Visible = true;

	public void Deselect() => Texture.Visible = false;
}