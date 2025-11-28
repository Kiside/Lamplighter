using System;
using Godot;
using SystemLamplighter;
using SystemLamplighter.Interfaces;
using MessagePipe;


public partial class ButtonUi : Button, IButtonUi
{
	public event Action<string> OnClick;


	public override void _Ready()
	{
		base._Ready();

		this.ButtonDown += Click;
	}

	public void Init(string name)
	{
		this.Name = name;
		this.Text = name;
	}

	public void SetMinimumSize(Vector2 size)
	{
		this.CustomMinimumSize = size;
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