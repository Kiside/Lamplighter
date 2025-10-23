using System;
using Godot;

namespace SystemLamplighter
{
	public class AtbCharacter
	{
		// I personaggi hanno un immagine di un piccolo avatar
		Image Avatar;
		// La velocità del personaggio sull'ATB
		int Speed;
		// Collegamento diretto con il personaggio
		AtbController Controller;
		// Posizione del personaggio all'interno dell'ATB
		float BarPosition;

		public string Name => Controller.Name;

		public AtbCharacter(Image avatar, int speed, AtbController controller)
		{
			Avatar = avatar;
			Speed = speed;
			Controller = controller;
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
