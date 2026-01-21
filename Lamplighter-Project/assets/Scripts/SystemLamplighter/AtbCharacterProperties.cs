using System;
using System.Diagnostics;
using Characters;
using Characters.Interfaces;
using Godot;
using MessagePipe;
using SystemLamplighter.Debug;
using SystemLamplighter.Events;
using SystemLamplighter.Extensions;

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
		// Il moltiplicatore alla velocità aggiunto dall'uso di un action
		[Export]
		float _speedMultiplier = 1;
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
		public float SpeedMultiplier {get {return _speedMultiplier;} set {_speedMultiplier = value;}}
		public float Position => _barPosition;
		public AtbCharacterStatus Status => _status;
		#endregion

		private readonly DisposableBagBuilder _bag = DisposableBag.CreateBuilder();
		

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

		public void Subscribe()
		{
			this.SubscribeEventResource<AtbEndExecuteActionEvent>(OnEndAction).AddTo(_bag);
		}

		public void Unsubscribe()
		{
			_bag?.Build().Dispose();
		}

		#region METHODS
		/// <summary>
		/// Metodo per la modifica della posizione del personaggio sulla barra
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public AtbCharacterStatus UpdatePosition(float value)
		{
			DebugLamplighter.Assert(value > 0, "speed is negative");

			if (CharacterType == AtbCharacterType.ALLY && _status == AtbCharacterStatus.COM)
				return _status;


			_barPosition += (_speed * _speedMultiplier) * value;
			_barPosition = Mathf.Clamp(_barPosition, 0, 1);

			return CheckPositionStatus();
		}

		/// <summary>
		/// Controlla se la posizione del personaggio è al threshold per il cambio status a command
		/// </summary>
		/// <returns></returns>
		private AtbCharacterStatus CheckPositionStatus()
		{
			if (_status == AtbCharacterStatus.CHARGE && 
			_barPosition >= Common.ATB_COMAND_THRESHOLD)
			{
				if(_characterType == AtbCharacterType.ALLY)
					_barPosition = Common.ATB_COMAND_THRESHOLD;
				_status = AtbCharacterStatus.COM;
				
			}

			if(_status == AtbCharacterStatus.CHARGE_ACTION && _barPosition >= Common.ATB_END)
			{
				
				_status = AtbCharacterStatus.ACTION;
			}

			
			return _status;
		}

		public void EndCommandStatus(float speedMultiplier) 
		{
			_speedMultiplier = speedMultiplier;
			_status = AtbCharacterStatus.CHARGE_ACTION;
		}

		/// <summary>
		/// Alla fine dell'azione avvenuta resetta le posizioni
		/// </summary>
		public void OnEndAction(AtbEndExecuteActionEvent ev)
		{
			if(ev.Actor.AtbProperties != this)
				return;
			
			
			_barPosition = 0f;
			_speedMultiplier = 1f;
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
