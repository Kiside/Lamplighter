using System;
using Godot;

namespace SystemLamplighter
{
	public abstract partial class AbstractController<TView, TModel> : Node
	where TView : Node
	where TModel : Node
	{
		[Export]
		public NodePath View;
		[Export]
		public NodePath Model;

		protected TView _view;
		protected TModel _model;

		public virtual void Init()
		{
			NodeChecking();
		}

		public override void _Ready()
		{
			base._Ready();

			Init();
		}

		public virtual void NodeChecking()
		{
			if (View is null || View is not TView)
				Log.PrintWarning("There is no View");
			else
				_view = GetNode<TView>(View);

			if (Model is null || Model is not TModel)
				Log.PrintWarning("There is no Model");
			else
				_model = GetNode<TModel>(Model);
		}
	}
}
