using System;
using Godot;

namespace SystemLamplighter
{
	public abstract partial class AbstractController<TView, TModel> : Node
	{
		protected TView _view;
		protected TModel _model;
	}
}
