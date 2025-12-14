using System;
using System.Collections.Generic;
using Godot;
using SystemLamplighter;
using System.Diagnostics;

namespace Characters
{
	/// <summary>
	/// Classe MVC per i personaggi
	/// </summary>
	/// <typeparam name="TView"></typeparam>
	/// <typeparam name="TModel"></typeparam>
	public partial class CharacterController<TView, TModel> : AbstractCharacterController 
	where TView : AbstractView
	where TModel : AbstractModel
	{
		[Export]
		public NodePath View;
		[Export]
		public NodePath Model;

		protected TView _view;
		protected TModel _model;

		public sealed override void Init()
		{
			NodeChecking();
			OnInit();
		}

		protected virtual void OnInit()
		{
			
		}

		protected override void NodeChecking()
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

		public virtual List<string> GetAttacksId() { return new List<string>();}

		public virtual List<string> GetMagicsId(){ return new List<string>();}
		public virtual List<string> GetItemsId() { return new List<string>();}

	}
}