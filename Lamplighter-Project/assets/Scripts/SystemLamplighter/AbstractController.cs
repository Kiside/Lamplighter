using System;
using Godot;
using System.Diagnostics;

namespace SystemLamplighter
{
	public abstract partial class AbstractController<TView, TModel> : Node
	where TView : AbstractView
	where TModel : AbstractModel
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
			
			_view = GetNode<TView>(View);
			_view.Init();

			_model = GetNode<TModel>(Model);
			_model.Init();

			Assert();
		}

		protected void Assert()
		{
			Debug.Assert(_model != null, "_model is null");
			Debug.Assert(_view != null, "_view is null");
		}
	}
}
