using System;
using Godot;

namespace SystemLamplighter
{
	public abstract partial class AbstractController<TView, TModel> : Node
	{
		protected TView _view;
		protected TModel _model;

		public virtual void Init()
		{
			if (_view is null)
				Log.PrintWarning("There is no View");

			if (_model is null)
				Log.PrintWarning("There is no Model");
		}

		public override void _Ready()
		{
			base._Ready();

			Init();
		}
	}
}
