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
		#region Export Variable	
		[Export]
		public NodePath ViewPath;
		[Export]
		public NodePath ModelPath;
		#endregion

		#region Protected variable
		protected TView _view;
		protected TModel _model;
		#endregion

		#region Public variable
		public TView View => _view;
		public TModel Model => _model;
		

		// TODO: DA METTERE NEL MODEL? FORSE INUTILE CHE SIA QUI?
		public Identification Id { get; private set; }
		#endregion

		#region  Methods
		public override void _EnterTree()
		{
			InitId();
			base._EnterTree();
		}
		
		/// <summary>
		/// Metodo per inizializzare la variabile di identificazione del personaggio
		/// </summary>
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

		protected virtual void Subscribe() {}
		protected virtual void Unsubscribe() {}

		#endregion

	}
}