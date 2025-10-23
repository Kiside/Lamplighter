using System;
using Godot;

namespace SystemLamplighter
{
	public abstract partial class AbstractController<TView, TModel> : Node
	{
		TView _view;
		TModel _model;
	}
}
