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
		int _speed;
		// Collegamento diretto con il personaggio
		AtbController _controller;
		// Posizione del personaggio all'interno dell'ATB
		float _barPosition;
		//
		AtbCharacterType _characterType;
		AtbCharacterStatus _status;

		AtbController Controller => _controller;
		public string Name => Controller.Name;
		public AtbCharacterType CharacterType => _characterType;
		public Image Avatar => _avatar;
		public int Speed => _speed;
		public float Position => _barPosition;
		AtbCharacterStatus Status => _status;

		public AtbCharacterStatus UpdatePosition(float value)
		{
			if (_status == AtbCharacterStatus.COM)
				return _status;

			_barPosition += value * _speed;
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

		public AtbCharacter(Image avatar, int speed, AtbController controller, AtbCharacterType atbCharacterType)
		{
			_avatar = avatar;
			_speed = speed;
			_controller = controller;
			_characterType = atbCharacterType;
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
