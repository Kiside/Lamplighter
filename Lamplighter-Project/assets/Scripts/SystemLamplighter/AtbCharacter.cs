using System;
using Characters;
using Godot;

namespace SystemLamplighter
{
	// Classe che costruisce e mantiene le info dei avatar sul ATB
	public class AtbCharacter
	{
		#region PRIVATE PROPERTIES
		// I personaggi hanno un immagine di un piccolo avatar
		Image _avatar;
		// La velocità del personaggio sull'ATB
		float _speed;
		// Collegamento diretto con il personaggio
		ICharacterControllerAtb _character;
		// Posizione del personaggio all'interno dell'ATB
		float _barPosition;
		// Name of Character 
		private string _name;
		// Il tipo del personaggio alleato o nemico
		AtbCharacterType _characterType;
		// Lo status del personaggio
		AtbCharacterStatus _status;
		#endregion

		#region PUBLIC PROPERTIES
		public string Name => _name;
		public AtbCharacterType CharacterType => _characterType;
		public Image Avatar => _avatar;
		public float Speed => _speed;
		public float Position => _barPosition;
		AtbCharacterStatus Status => _status;
		#endregion

		#region CONSTRUCTOR
		public AtbCharacter(Image avatar, float speed, string name, AtbCharacterType atbCharacterType)
		{
			_avatar = avatar;
			_speed = speed;
			_name = name;
			_characterType = atbCharacterType;
			_status = AtbCharacterStatus.CHARGE;
		}
		#endregion

		#region METHODS
		/// <summary>
		/// Metodo per la modifica della posizione del personaggio sulla barra
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public AtbCharacterStatus UpdatePosition(float value)
		{
			if (_status == AtbCharacterStatus.COM)
				return _status;

			_barPosition += _speed * value;
			_barPosition = Mathf.Clamp(_barPosition, 0, 1);

			return CheckPositionStatus();
		}

		/// <summary>
		/// Controlla se la posizione del personaggio è al threshold per il cambio status a command
		/// </summary>
		/// <returns></returns>
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

		/// <summary>
		/// Resetta le posizioni
		/// </summary>
		public void ResetAtbPosition()
		{
			_barPosition = 0f;
			_status = AtbCharacterStatus.CHARGE;
		}


		/// <summary>
		/// Chiama l'azione del personaggio
		/// </summary>
		public void Action()
		{
			_character.AtbAction();
		}
		#endregion
	}
}
