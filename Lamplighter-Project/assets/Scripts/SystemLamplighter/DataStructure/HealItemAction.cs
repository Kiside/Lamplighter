using System;
using System.Diagnostics;
using Godot;

namespace SystemLamplighter.DataStructure
{
	[GlobalClass]
	public partial class HealItemAction : ItemAction
	{
		[Export]
		private float _healAmount;
		public float HealAmount => _healAmount;

		public HealItemAction() : this(0) { }

		public HealItemAction(float healAmount)
		{
			Debug.Assert(healAmount > 0, "healAmount is negative");
			_healAmount = healAmount;
		}
	}
}