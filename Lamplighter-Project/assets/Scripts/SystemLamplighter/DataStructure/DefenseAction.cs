using System;
using Godot;

namespace SystemLamplighter.DataStructure
{
	[GlobalClass]
	public partial class DefenseAction : ActionData
	{
		[Export]
		private float _defensiveAmount;

		public float DefensiveAmount => _defensiveAmount;

		public DefenseAction() : this(0f) { }

		public DefenseAction(float defensiveAmount)
		{
			_actionType = ActionType.GUARD;
			_defensiveAmount = defensiveAmount;
		}
	}
}