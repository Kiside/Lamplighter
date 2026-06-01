using Characters.Interfaces;
using Godot;
using MessagePipe;
using SystemLamplighter.ATB;
using SystemLamplighter.Common.Constants;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.Debug;
using SystemLamplighter.Events;

public class AtbCharacterService : IAtbCharacterService
{
	public CharacterProperties CharacterProperties { get; private set; }
	public ISubscriber<AtbEndExecuteActionEvent> _subscriberAtbEndExecuteActionEvent;
	public ISubscriber<AtbCommandPhaseEndEvent> _subscribeAtbCommandPhaseEndEvent;
	private AtbCharacterType CharacterType => CharacterProperties.AtbCharacterType;
	private AtbCharacterStatus _status;

	float _barPosition
	{
		get { return CharacterProperties.AtbBarPosition; }
		set { CharacterProperties.AtbBarPosition = value; }
	}
	float _speed => CharacterProperties.AtbSpeed;
	float _speedMultiplier
	{
		get { return CharacterProperties.AtbSpeedMultiplier; }
		set { CharacterProperties.AtbSpeedMultiplier = value; }
	}
	private AtbCharacterProperties AtbCharacterProperties
	=> CharacterProperties.AtbCharacterProperties;


	private readonly DisposableBagBuilder _bag = DisposableBag.CreateBuilder();


	public void Init(CharacterProperties characterProperties,
	ISubscriber<AtbEndExecuteActionEvent> subscriberAtbEndExecuteActionEvent,
	ISubscriber<AtbCommandPhaseEndEvent> subscribeAtbCommandPhaseEndEvent)
	{
		DebugLamplighter.Assert(characterProperties != null, "characterProperties is null");

		CharacterProperties = characterProperties;
		_subscriberAtbEndExecuteActionEvent = subscriberAtbEndExecuteActionEvent;
		_subscribeAtbCommandPhaseEndEvent = subscribeAtbCommandPhaseEndEvent;
	}

	public AtbCharacterStatus CheckPositionStatus()
	{
		if (_status == AtbCharacterStatus.CHARGE &&
			_barPosition >= AtbConstants.ATB_COMAND_THRESHOLD)
		{
			if (CharacterType == AtbCharacterType.ALLY)
				_barPosition = AtbConstants.ATB_COMAND_THRESHOLD;
			_status = AtbCharacterStatus.COM;
		}

		if (_status == AtbCharacterStatus.CHARGE_ACTION && _barPosition >= AtbConstants.ATB_END)
		{
			_status = AtbCharacterStatus.ACTION;
		}


		return _status;
	}

	public void OnEndCommandStatus(AtbCommandPhaseEndEvent ev)
	{
		if (ev.Actor.AtbProperties != AtbCharacterProperties)
			return;
			
		_speedMultiplier = ev.Actor.CurrentAction.ActionSpeedMultiplier;
		_status = AtbCharacterStatus.CHARGE_ACTION;
	}

	public void OnEndAction(AtbEndExecuteActionEvent ev)
	{
		if (ev.Actor.AtbProperties != AtbCharacterProperties)
			return;


		_barPosition = 0f;
		_speedMultiplier = 1f;
		_status = AtbCharacterStatus.CHARGE;
	}

	public void Subscribe()
	{
		_subscriberAtbEndExecuteActionEvent.Subscribe(OnEndAction).AddTo(_bag);
		_subscribeAtbCommandPhaseEndEvent.Subscribe(OnEndCommandStatus).AddTo(_bag);
	}

	public void Unsubscribe()
	{
		_bag?.Build().Dispose();
	}

	public AtbCharacterStatus UpdatePosition(float value)
	{
		DebugLamplighter.Assert(value > 0, "speed is negative");

		if (CharacterType == AtbCharacterType.ALLY && _status == AtbCharacterStatus.COM)
			return _status;


		_barPosition += _speed * _speedMultiplier * value;
		_barPosition = Mathf.Clamp(_barPosition, 0, 1);

		return CheckPositionStatus();
	}

	public void Dispose()
	{
		Unsubscribe();
	}
}