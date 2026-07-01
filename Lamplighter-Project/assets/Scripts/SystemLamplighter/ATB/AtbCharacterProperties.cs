using System;
using Characters.Interfaces;
using Godot;
using MessagePipe;
using SystemLamplighter.ATB.Interfaces;
using SystemLamplighter.Debug;
using SystemLamplighter.Events;
using SystemLamplighter.Extensions;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.Common.Constants;


namespace SystemLamplighter.ATB
{
	[GlobalClass]
	// Resource che costruisce e mantiene le info dei avatar sull' ATB
	public partial class AtbCharacterProperties : Resource, IAtbCharacterProperties
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
		public float Position {get {return _barPosition;} set {_barPosition = value;}}
		public AtbCharacterStatus Status => _status;
		#endregion

		private readonly DisposableBagBuilder _bag = DisposableBag.CreateBuilder();
		
	}
}
