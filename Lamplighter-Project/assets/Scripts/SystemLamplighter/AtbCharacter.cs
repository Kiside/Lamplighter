using System;
using Godot;

namespace SystemLamplighter
{
	// Classe che costruisce e mantiene le info dei avatar sul ATB
	public class AtbCharacter
	{
		// I personaggi hanno un immagine di un piccolo avatar
		Image _avatar;
		// La velocità del personaggio sull'ATB
		float _speed;
		// Collegamento diretto con il personaggio
		AtbCharacterController _controller;
		// Posizione del personaggio all'interno dell'ATB
		float _barPosition;
		//
		AtbCharacterType _characterType;
		AtbCharacterStatus _status;

		AtbCharacterController Controller => _controller;
		public string Name => Controller.Name;
		public AtbCharacterType CharacterType => _characterType;
		public Image Avatar => _avatar;
		public float Speed => _speed;
		public float Position => _barPosition;
		AtbCharacterStatus Status => _status;

		public AtbCharacterStatus UpdatePosition(float value)
		{
			if (_status == AtbCharacterStatus.COM)
				return _status;

			_barPosition += _speed * value;
			_barPosition = Mathf.Clamp(_barPosition, 0, 1);

			return CheckPositionStatus();
		}

		private AtbCharacterStatus CheckPositionStatus()
		{
			if (_status == AtbCharacterStatus.CHARGE)
			{
				if (_barPosition >= Common.ATB_COMAND_THRESHOLD)
				{
					_barPosition = Common.ATB_COMAND_THRESHOLD;
					_status = AtbCharacterStatus.COM;
				}
			}

			return _status;
		}

		public void ResetAtbPosition()
		{
			_barPosition = 0f;
			_status = AtbCharacterStatus.CHARGE;
		}

		public AtbCharacter(Image avatar, float speed, AtbCharacterController controller, AtbCharacterType atbCharacterType)
		{
			_avatar = avatar;
			_speed = speed;
			_controller = controller;
			_characterType = atbCharacterType;
			_status = AtbCharacterStatus.CHARGE;
		}

		/// <summary>
		/// Chiama l'azione del personaggio
		/// </summary>
		public void Action()
		{
			_controller.Action();
		}
	}
}
