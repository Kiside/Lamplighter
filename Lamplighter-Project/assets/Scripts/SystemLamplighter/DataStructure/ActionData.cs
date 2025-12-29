using System;
using System.Diagnostics;
using Godot;

namespace SystemLamplighter.DataStructure
{
	[GlobalClass]
	public partial class ActionData : Resource, IActionData
	{
		// Che tipo di azione è
		[Export]
		protected ActionType _actionType;
		// Il nome dell'azione
		[Export]
		protected string _name;
		// La velocità che avrà l'azione
		[Export]
		protected float _actionSpeed;

		public ActionType ActionType => _actionType;
		public string Name => _name;
		public float ActionSpeed => _actionSpeed;

		public ActionData() : this(ActionType.ATTACK, "", 0f) { }

		public ActionData(ActionType actionType, string name, float actionSpeed)
		{
			_actionType = actionType;
			_name = name;
			_actionSpeed = actionSpeed;
		}
	}
}