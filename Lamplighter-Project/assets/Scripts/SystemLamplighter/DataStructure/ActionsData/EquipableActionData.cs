using System;
using System.Diagnostics;
using Godot;

namespace SystemLamplighter.DataStructure.ActionsData
{
	/// <summary>
	/// Questa classe serve per specificare se una action è una action da equipaggiare o no
	/// </summary>
	[GlobalClass]
	public partial class EquipableActionData : ActionData
	{
		[Export]
		protected bool _isEquipped;

		public bool IsEquipped;

		public EquipableActionData() : this(false) { }

		public EquipableActionData(bool isEquipped)
		{
			_isEquipped = isEquipped;
		}
	}
}