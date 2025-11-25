using System;
using Godot;
using SystemLamplighter;
using SystemLamplighter.Interfaces;
using MessagePipe;


public partial class ButtonUi : Button, IButtonUi
{
	public event Action<string> OnClick;

	public void Init(string name)
	{
		this.Name = name;
		this.Text = name;
		this.CustomMinimumSize = new Vector2(251, 60);

		this.ButtonDown += Click;
	}

	public void Click()
	{
		OnClick?.Invoke(this.Name);
	}

	public override void _ExitTree()
	{
		base._ExitTree();
		this.ButtonDown -= Click;
	}
}