using Godot;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.Debug;
using SystemLamplighter.Events;

public class AtbCharacterService : IAtbCharacterService
{
	public CharacterProperties CharacterProperties {get; private set;}

	private AtbCharacterType CharacterType;
	private AtbCharacterStatus _status;
	float _barPosition;
	float _speed => CharacterProperties.AtbSpeed;
	float _speedMultiplier => CharacterProperties.AtbSpeedMultiplier;

	public void Init(CharacterProperties characterProperties)
	{
		CharacterProperties = characterProperties;
	}

	public AtbCharacterStatus CheckPositionStatus()
	{
		throw new System.NotImplementedException();
	}

	public void EndCommandStatus(float speedMultiplier)
	{
		throw new System.NotImplementedException();
	}

	public void OnEndAction(AtbEndExecuteActionEvent ev)
	{
		throw new System.NotImplementedException();
	}

	public void Subscribe()
	{
		throw new System.NotImplementedException();
	}

	public void Unsubscribe()
	{
		throw new System.NotImplementedException();
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
}