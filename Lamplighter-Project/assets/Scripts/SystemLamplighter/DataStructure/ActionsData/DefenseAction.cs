using System;
using System.Diagnostics;
using Godot;
using SystemLamplighter.Common.Enums;

namespace SystemLamplighter.DataStructure.ActionsData
{
	/// <summary>
	/// Classe per l'azione difensiva
	/// </summary>
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