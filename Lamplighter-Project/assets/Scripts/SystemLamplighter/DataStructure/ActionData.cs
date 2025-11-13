using System;

namespace SystemLamplighter.DataStructure
{
	public abstract class ActionData
	{
		// Che tipo di azione è
		protected ActionType _actionType;
		// Il nome dell'azione
		protected string _name;
		// La velocità che avrà l'azione
		protected float _actionSpeed;

		public ActionType ActionType => _actionType;
		public string Name => _name;
		public float ActionSpeed => _actionSpeed;
	}
}