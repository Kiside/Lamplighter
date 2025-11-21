using System;
using Godot;
using SystemLamplighter;
using SystemLamplighter.Interfaces;
using MessagePipe;


public partial class ButtonUi : Button, IButtonUi
{
	private IPublisher<ActionEvent> _publisher { get; set; }

	[Export]
	EventType.UIEvent _uiEvent;
	private string _payLoad;

	public void Init(string payLoad)
	{
		_payLoad = payLoad;
		this.Name = _uiEvent.ToString();
	}

	public override void _Pressed()
	{
		OnClick();
	}

	public void OnClick()
	{
		_publisher?.Publish(new ActionEvent(_uiEvent, _payLoad));
	}
}