using System;
using Godot;

namespace SystemLamplighter
{
	public abstract partial class AbstractView : Control
	{
		public abstract void Init();

		public override void _Ready()
		{
			base._Ready();

			Init();
		}
	}
}
