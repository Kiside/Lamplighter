using System;
using System.Collections.Generic;
using Godot;
using SystemLamplighter;
using System.Diagnostics;
using Characters.Abstract;
using SystemLamplighter.Abstract.MVC;
using SystemLamplighter.Interfaces;

namespace Characters
{
	/// <summary>
	/// Classe MVC per i personaggi
	/// </summary>
	/// <typeparam name="TView"></typeparam>
	/// <typeparam name="TModel"></typeparam>
	public partial class CharacterController<TView, TModel> : AbstractCharacterController, IIdentificable
	where TView : AbstractView
	where TModel : AbstractModel
	{
		[Export]
		public NodePath ViewPath;
		[Export]
		public NodePath ModelPath;

		protected TView _view;
		protected TModel _model;

		public TView View => _view;
		public TModel Model => _model;

		// TODO: DA METTERE NEL MODEL? FORSE INUTILE CHE SIA QUI?
		public Identification Id { get; private set; }

		public override void _EnterTree()
		{
			InitId();
			base._EnterTree();
		}
		
		protected void InitId()
		{
			if(Id == null)
			{
				Id = new Identification(this.Name);
			}
		}

		/// <summary>
		/// Prima implementazione basilare di Init()
		/// </summary>
		public sealed override void Init()
		{
			NodeChecking();
			//InitId();
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
			_view = GetNode<TView>(ViewPath);
			_view.Init();

			_model = GetNode<TModel>(ModelPath);
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

		protected virtual void Subscribe() {}
		protected virtual void Unsubscribe() {}

	}
}