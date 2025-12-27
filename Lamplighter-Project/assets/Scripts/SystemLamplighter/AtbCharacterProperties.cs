using System;
using System.Diagnostics;
using Characters;
using Godot;
using SystemLamplighter.Debug;

namespace SystemLamplighter
{
	[GlobalClass]
	// Classe che costruisce e mantiene le info dei avatar sul ATB
	public partial class AtbCharacterProperties : Resource
	{
		#region PRIVATE PROPERTIES
		// I personaggi hanno un immagine di un piccolo avatar
		[Export]
		Image _avatar;
		// La velocità del personaggio sull'ATB
		[Export]
		float _speed;
		// Posizione del personaggio all'interno dell'ATB
		float _barPosition;
		// Name of Character 
		[Export]
		private string _name;
		// Il tipo del personaggio alleato o nemico
		[Export]
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
		public AtbCharacterStatus Status => _status;
		#endregion

		#region CONSTRUCTOR
		public void Init(Image avatar, float speed, string name, AtbCharacterType atbCharacterType,ICharacterControllerAtb character)
		{
			DebugLamplighter.Assert(avatar != null, "avatar is null");
			DebugLamplighter.Assert(name != null, "name is null");
			DebugLamplighter.Assert(name != String.Empty, "name is empty");
			DebugLamplighter.Assert(speed > 0, "speed is negative");
			DebugLamplighter.Assert(character != null, "character is null");

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
			DebugLamplighter.Assert(value > 0, "speed is negative");

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
			//_character.AtbAction();
		}
		#endregion
	}
}
