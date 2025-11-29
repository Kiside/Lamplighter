using System;
using Godot;
using SystemLamplighter;
using SystemLamplighter.Interfaces;
using MessagePipe;
using System.Diagnostics;


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
		Debug.Assert(name != null, "name is null");
		Debug.Assert(name != String.Empty, "name is empty");

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