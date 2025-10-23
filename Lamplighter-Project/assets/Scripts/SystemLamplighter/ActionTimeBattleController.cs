using Godot;
using System;
using System.Collections.Generic;


namespace SystemLamplighter
{
	public partial class ActionTimeBattleController() : AbstractController<ActionTimeBattleView, ActionTimeBattleModel>
	{
		private List<AtbCharacter> _charactersInCombat;

		public override void _PhysicsProcess(double delta)
		{
			ClockingAtb();
		}

		/// <summary>
		/// Operazioni per far scorrere l'ATB
		/// </summary>
		private void ClockingAtb()
		{

		}

		/// <summary>
		/// Avvio del momento Comand
		/// </summary>
		private void CommandAtb()
		{

		}

		/// <summary>
		/// Avvio del richiamo per far effettuare l'azione al personaggio
		/// </summary>
		private void Action(AtbCharacter character)
		{
			character.Action();
		}


		/// <summary>
		/// Aggiunge un personaggio alla lista
		/// </summary>
		/// <param name="character">personaggio</param>
		public void AddCharacter(AtbCharacter character)
		{
			_model.AddCharacter(character);
		}

		/// <summary>
		/// Pulisce l'intera lista
		/// </summary>
		public void ClearCharactersInCombat()
		{
			_model.ClearCharacters();
		}

		/// <summary>
		/// Rimuove un personaggio dalla lista
		/// </summary>
		/// <param name="character"></param>
		public void RemoveCharacterInCombat(AtbCharacter character)
		{
			_model.RemoveCharacter(character);
		}
	}
}
