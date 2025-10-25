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
		int Speed;
		// Collegamento diretto con il personaggio
		AtbController Controller;
		// Posizione del personaggio all'interno dell'ATB
		float BarPosition;
		//
		AtbCharacterType _characterType;

		public string Name => Controller.Name;
		public AtbCharacterType CharacterType => _characterType;
		public Image Avatar => _avatar;


		public AtbCharacter(Image avatar, int speed, AtbController controller, AtbCharacterType atbCharacterType)
		{
			_avatar = avatar;
			Speed = speed;
			Controller = controller;
			_characterType = atbCharacterType;
		}

		/// <summary>
		/// Chiama l'azione del personaggio
		/// </summary>
		public void Action()
		{
			Controller.Action();
		}
	}
}
