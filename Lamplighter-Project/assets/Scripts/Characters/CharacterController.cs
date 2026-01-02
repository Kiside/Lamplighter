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
		

		/// <summary>
		/// Prima implementazione basilare di Init()
		/// </summary>
		public sealed override void Init()
		{
			NodeChecking();
			OnInit();
		}

		/// <summary>
		/// Metodo per l'inizializzazione post Init()
		/// </summary>
		protected virtual void OnInit()
		{
			
		}

		/// <summary>
		/// Metodo base del NodeChecking() che controlla le variabili del Model e della View
		/// </summary>
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

		public virtual List<IActionData> GetAttacksId() { return new List<IActionData>();}

		public virtual List<IActionData> GetMagicsId(){ return new List<IActionData>();}
		public virtual List<IActionData> GetItemsId() { return new List<IActionData>();}
		public virtual List<IActionData> GetDefenseId() {return new List<IActionData>();}

	}
}