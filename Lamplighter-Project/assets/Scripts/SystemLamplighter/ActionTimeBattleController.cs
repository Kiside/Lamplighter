using Godot;
using System;
using System.Collections.Generic;


namespace SystemLamplighter
{
	public partial class ActionTimeBattleController() : AbstractController<ActionTimeBattleView, ActionTimeBattleModel>
	{
		private List<AtbCharacter> _charactersInCombat;

		bool _inCharging = true;
		int _currentIndex;

		public override void Init()
		{
			base.Init();

			_currentIndex = 0;
		}

		public override void _PhysicsProcess(double delta)
		{
			ClockingAtb(delta);
		}

		/// <summary>
		/// Operazioni per far scorrere l'ATB
		/// </summary>
		private void ClockingAtb(double delta)
		{
			//processa la posizione per ogni personaggio
			// Se lo stato di tutti i personaggi è in charging allora il ciclo può continuare
			if (_inCharging)
			{
				// _currentIndex mantiene l'ultimo indice che è stato controllato
				int i = _currentIndex;
				while (i < _charactersInCombat.Count)
				{
					float pos = Common.ToSingle(_charactersInCombat[i].Position);
					// Se un personaggio entra in Command
					if (_charactersInCombat[i].UpdatePosition(pos) == AtbCharacterStatus.COM)
					{
						// Bisogna evitare il continuo del ciclo è "fermare" il proseguimento dell'ATB
						_inCharging = false;
						_currentIndex = i;
						CommandAtb();
						break;
					}
					//Chiedo alla view di riposizionare i vari personaggi
					_view.UpdatePosition(pos, i);
					i++;
				}
				_currentIndex = 0;
			}

		}

		/// <summary>
		/// Avvio del momento Comand
		/// </summary>
		private void CommandAtb()
		{

		}

		/// <summary>
		/// Metodo per far continuare a ciclare l'ATB dopo che l'utente ha selezionato il comand
		/// </summary>
		public void ContinueAtb()
		{
			_inCharging = true;
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
