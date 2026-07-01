using Characters.Interfaces;
using Godot;
using MessagePipe;
using SystemLamplighter.ATB;
using SystemLamplighter.Common.Constants;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.Debug;
using SystemLamplighter.Events;

namespace SystemLamplighter.ATB.Interfaces;

/// <summary>
/// Classe con le logiche business del personaggio sull'ATB
/// </summary>
public class AtbCharacterService : IAtbCharacterService
{
	#region  Public Variables
	public CharacterProperties CharacterProperties { get; private set; }
	public ISubscriber<AtbEndExecuteActionEvent> _subscriberAtbEndExecuteActionEvent;
	public ISubscriber<AtbCommandPhaseEndEvent> _subscribeAtbCommandPhaseEndEvent;
	#endregion 

	#region Private Variables
	private AtbCharacterType CharacterType => CharacterProperties.AtbCharacterType;
	private AtbCharacterStatus _status;

	private float _barPosition
	{
		get { return CharacterProperties.AtbBarPosition; }
		set { CharacterProperties.AtbBarPosition = value; }
	}
	private float _speed => CharacterProperties.AtbSpeed;

	private float _speedMultiplier
	{
		get { return CharacterProperties.AtbSpeedMultiplier; }
		set { CharacterProperties.AtbSpeedMultiplier = value; }
	}

	private AtbCharacterProperties AtbCharacterProperties
	=> CharacterProperties.AtbCharacterProperties;


	private readonly DisposableBagBuilder _bag = DisposableBag.CreateBuilder();
	#endregion

	#region Methods
	public void Init(CharacterProperties characterProperties,
	ISubscriber<AtbEndExecuteActionEvent> subscriberAtbEndExecuteActionEvent,
	ISubscriber<AtbCommandPhaseEndEvent> subscribeAtbCommandPhaseEndEvent)
	{
		DebugLamplighter.Assert(characterProperties != null, "characterProperties is null");

		CharacterProperties = characterProperties;
		_subscriberAtbEndExecuteActionEvent = subscriberAtbEndExecuteActionEvent;
		_subscribeAtbCommandPhaseEndEvent = subscribeAtbCommandPhaseEndEvent;
	}

	/// <summary>
	/// Metodo per Controllare lo stato del personaggio sull'ATB (IN CARICA, COMMAND, AZIONE IN CARICA, AZIONE  )
	/// </summary>
	/// <returns></returns>
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

	/// <summary>
	/// Metodo chiamato quando arriva l'evento che lo stato "Command" è concluso
	/// </summary>
	/// <param name="ev"></param>
	public void OnEndCommandStatus(AtbCommandPhaseEndEvent ev)
	{
		if (ev.Actor.AtbProperties != AtbCharacterProperties)
			return;
			
		_speedMultiplier = ev.Actor.CurrentAction.ActionSpeedMultiplier;
		_status = AtbCharacterStatus.CHARGE_ACTION;
	}

	/// <summary>
	/// Metodo chiamato quando arriva l'evento che l'Azione si è conclusa
	/// </summary>
	/// <param name="ev"></param>
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


	/// <summary>
	/// Metodo che aggiorna la posizione del personaggio sull'ATB
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
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
	#endregion
}