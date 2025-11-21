using System;
using Godot;
using SystemLamplighter.Interfaces;

namespace SystemLamplighter
{
	[Serializable]
	public class ActionEvent
	{
		EventType.UIEvent _event { get; set; }
		string _payLoad { get; set; }

		public ActionEvent(EventType.UIEvent _event, string _payLoad)
		{
			this._event = _event;
			this._payLoad = _payLoad;
		}
	}
}