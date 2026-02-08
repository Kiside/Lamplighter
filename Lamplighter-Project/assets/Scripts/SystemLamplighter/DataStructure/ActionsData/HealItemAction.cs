using System;
using System.Diagnostics;
using Godot;

namespace SystemLamplighter.DataStructure.ActionsData
{
	// ! DA CANCELLARE E REWORKARE IL SISTEMA
	/// <summary>
	/// Classe per l'azione di cura di un item
	/// </summary>
	[GlobalClass]
	public partial class HealItemAction : ItemAction
	{
		[Export]
		private float _healAmount;
		public float HealAmount => _healAmount;

		public HealItemAction() : this(0) { }

		public HealItemAction(float healAmount)
		{
			_healAmount = healAmount;
		}
	}
}