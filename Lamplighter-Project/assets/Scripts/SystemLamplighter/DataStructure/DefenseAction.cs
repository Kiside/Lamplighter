using System;
using System.Diagnostics;
using Godot;

namespace SystemLamplighter.DataStructure
{
	[GlobalClass]
	public partial class DefenseAction : ActionData
	{
		[Export]
		private float _defensiveAmount;

		public float DefensiveAmount => _defensiveAmount;

		public DefenseAction() : this(0f, 10f) { }

		public DefenseAction(float defensiveAmount, float speed)
		{
			_actionType = ActionType.GUARD;
			_defensiveAmount = defensiveAmount;
			_actionSpeedMultiplier = speed;
		}
	}
}