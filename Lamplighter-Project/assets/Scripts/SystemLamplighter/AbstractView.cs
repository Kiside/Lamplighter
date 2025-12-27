using System;
using Godot;

namespace SystemLamplighter
{
	public abstract partial class AbstractView : Node
	{
		public virtual bool Visible
		{
			get => false;
			set {}
		}
		public abstract void Init();

		public override void _Ready()
		{
			base._Ready();

			Init();
		}
	}
}
