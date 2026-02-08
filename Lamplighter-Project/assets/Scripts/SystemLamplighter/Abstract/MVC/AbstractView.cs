using System;
using Godot;

namespace SystemLamplighter.Abstract.MVC
{
	/// <summary>
	/// Classe astratta per la view
	/// </summary>
	public abstract partial class AbstractView : Node
	{
		/// <summary>
		/// Propietà di visibilità, pensata per dare due metodi diversi per controllare la visibilità
		/// di view UI e view nel piano 3D
		/// </summary>
		public virtual bool Visible
		{
			get => false;
			set {}
		}
		/// <summary>
		/// Metodo per l'inizializzazione della View
		/// </summary>
		public abstract void Init();

		public override void _Ready()
		{
			base._Ready();

			Init();
		}
	}
}
