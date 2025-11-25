using Godot;
using System;
using System.Collections.Generic;


namespace SystemLamplighter.ATB
{
	public partial class ActionTimeBattleController() : AbstractController<ActionTimeBattleView, ActionTimeBattleModel>
	{
		bool _inCharging = false;
		int _currentIndex;

		public event Action OnCommandEvent;

		// PER DEBUG
		public void CallViewUpdatePosition(float position, int index) => _view.UpdatePosition(position, index);

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
			// Se lo stato di tutti i personaggi è in charging e ci sono personaggi nell'atb allora il ciclo può continuare
			if (_inCharging && _model.CharactersCount > 0)
			{
				// _currentIndex mantiene l'ultimo indice che è stato controllato
				int i = _currentIndex;
				while (i < _model.CharactersCount)
				{
					// Se un personaggio entra in Command
					if (_model.Characters[i].UpdatePosition((float)delta) == AtbCharacterStatus.COM)
					{
						// Bisogna evitare il continuo del ciclo è "fermare" il proseguimento dell'ATB
						_inCharging = false;
						_currentIndex = i;
						CommandAtb();
						break;
					}
					//Chiedo alla view di riposizionare i vari personaggi
					_view.UpdatePosition(_model.Characters[i].Position, i);
					i++;
				}
				_currentIndex = 0;
			}
			else if (_inCharging && _model.CharactersCount <= 0)
			{
				Log.PrintMessage("InCharge ma nessun personaggio");
			}

		}

		/// <summary>
		/// Avvio del momento Comand
		/// </summary>
		private void CommandAtb()
		{
			Log.PrintMessage("Sto in command");
			OnCommandEvent?.Invoke();
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
			_view.AddCharacter(character);
		}

		/// <summary>
		/// Pulisce l'intera lista
		/// </summary>
		public void ClearCharactersInCombat()
		{
			_model.ClearCharacters();
			_view.ClearCharacters();
		}

		/// <summary>
		/// Rimuove un personaggio dalla lista
		/// </summary>
		/// <param name="character"></param>
		public void RemoveCharacterInCombat(AtbCharacter character)
		{
			_model.RemoveCharacter(character);
			_view.RemoveCharacter();
		}

		public void StopAtb()
		{
			_inCharging = false;
			ClearCharactersInCombat();
			_currentIndex = 0;
		}
	}
}
