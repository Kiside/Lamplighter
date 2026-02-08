using System;
using Godot;

namespace SystemLamplighter.Abstract.MVC
{
	/// <summary>
	/// Classe astratta per il model
	/// </summary>
	public abstract partial class AbstractModel : Node
	{
		public override void _Ready()
		{
			base._Ready();

			Init();
		}

		/// <summary>
		/// Metodo per inizializzare il model
		/// </summary>
		public abstract void Init();
	}
}
