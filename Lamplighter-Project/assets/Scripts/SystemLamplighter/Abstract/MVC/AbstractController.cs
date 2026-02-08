using System;
using Godot;
using SystemLamplighter.Debug;

namespace SystemLamplighter.Abstract.MVC
{
	/// <summary>
	/// Classe astratta per il controller
	/// </summary>
	/// <typeparam name="TView">View</typeparam>
	/// <typeparam name="TModel">Model</typeparam>
	public abstract partial class AbstractController<TView, TModel> : Node
	where TView : AbstractView
	where TModel : AbstractModel
	{
		/// <summary>
		/// Percorso della view
		/// </summary>
		[Export]
		public NodePath View;
		/// <summary>
		/// Percorso del model
		/// </summary>
		[Export]
		public NodePath Model;

		/// <summary>
		/// Parametro per la view
		/// </summary>
		protected TView _view;
		/// <summary>
		/// Parametro per il model
		/// </summary>
		protected TModel _model;

		/// <summary>
		/// Metodo base di inizializzazione del Controller
		/// </summary>
		public virtual void Init()
		{
			NodeChecking();
		}
		
		public override void _Ready()
		{
			base._Ready();

			Init();
		}

		/// <summary>
		/// Metodo per l'inizializzazione di View e Model di base
		/// </summary>
		public virtual void NodeChecking()
		{
			
			_view = GetNode<TView>(View);
			_view.Init();

			_model = GetNode<TModel>(Model);
			_model.Init();

			Assert();
		}


		/// <summary>
		/// Assert di base del controller
		/// </summary>
		protected void Assert()
		{
			DebugLamplighter.Assert(_model != null, "_model is null");
			DebugLamplighter.Assert(_view != null, "_view is null");
		}
	}
}
