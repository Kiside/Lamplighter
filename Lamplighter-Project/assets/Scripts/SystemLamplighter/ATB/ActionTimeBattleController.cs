using Characters.Interfaces;
using Godot;
using MessagePipe;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using SystemLamplighter.Debug;
using SystemLamplighter.Events;
using SystemLamplighter.Interfaces;
using SystemLamplighter.Extensions;


namespace SystemLamplighter.ATB
{
	public partial class ActionTimeBattleController() : AbstractController<ActionTimeBattleView, ActionTimeBattleModel>, IDisposable
	{
		bool _inCharging = false;
		int _currentIndex;

		public event Action OnCommandEvent;

		private readonly DisposableBagBuilder _bag = DisposableBag.CreateBuilder();
		#nullable enable
		private IDisposable? _atbCommandEndSubscription;
		#nullable disable
		private ICombatActor _currentActorInCommand;

		// PER DEBUG
		public void CallViewUpdatePosition(float position, int index) => _view.UpdatePosition(position, index);

		public override void Init()
		{
			base.Init();

			BattleManager.TriggerOnStartCombat += StartCombat;
			_currentIndex = 0;
		}

		public void StartCombat(List<ICombatActor> combatActors)
		{			
			AddCharacters(combatActors);
			ActivateATB(true);
		}

		public void ActivateATB(bool value)
		{
			_inCharging = value;
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
			DebugLamplighter.Assert(_model != null, "character is null");
			//processa la posizione per ogni personaggio
			// Se lo stato di tutti i personaggi è in charging e ci sono personaggi nell'atb allora il ciclo può continuare
			if (_inCharging && _model.CharactersCount > 0)
			{
				// _currentIndex mantiene l'ultimo indice che è stato controllato
				int i = _currentIndex;
				while (i < _model.CharactersCount)
				{
					var currentCharacter = _model.Characters[i];
					Log.PrintMessage($"status: {currentCharacter.AtbProperties.Status}");
					if(currentCharacter.AtbProperties.Status == AtbCharacterStatus.CHARGE || currentCharacter.AtbProperties.Status == AtbCharacterStatus.CHARGE_ACTION)
					{
						switch(currentCharacter.AtbProperties.UpdatePosition((float)delta))
						{
							// Se un personaggio entra in Command
							case AtbCharacterStatus.COM:
							// Bisogna evitare il continuo del ciclo è "fermare" il proseguimento dell'ATB
							_inCharging = false;
							_currentIndex = i;
							_currentActorInCommand = currentCharacter;
							_atbCommandEndSubscription = this.SubscribeEvent<AtbCommandPhaseEndEvent>(OnCommandEnd);
							_atbCommandEndSubscription.AddTo(_bag);
							this.PublishEvent(new AtbCommandPhaseStartedEvent(currentCharacter));
							break;
							// In stato Action
							case AtbCharacterStatus.ACTION:
							// Bisogna chiamare l'eseguimento dell'azione
							this.PublishEvent(new AtbExecuteActionEvent(currentCharacter));
							break;
						}
					}
						//Chiedo alla view di riposizionare i vari personaggi
						_view.UpdatePosition(_model.Characters[i].AtbProperties.Position, i);
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
		public void OnCommandEnd(AtbCommandPhaseEndEvent ev)
		{
			DebugLamplighter.Assert(ev != null, "ev is null");
			DebugLamplighter.Assert(_currentActorInCommand != null, "_currentActorInCommand is null");

			_atbCommandEndSubscription?.Dispose();

			_currentActorInCommand.AtbProperties.EndCommandStatus(ev.Actor.AtbProperties.SpeedMultiplier);
			_currentActorInCommand = null;
			
			_inCharging = true;
		}

		/// <summary>
		/// Avvio del richiamo per far effettuare l'azione al personaggio
		/// </summary>
		private void Action(AtbCharacterProperties character)
		{
			DebugLamplighter.Assert(character != null, "character is null");
			character.Action();
		}


		/// <summary>
		/// Aggiunge un personaggio alla lista
		/// </summary>
		/// <param name="character">personaggio</param>
		public void AddCharacter(ICombatActor character)
		{
			DebugLamplighter.Assert(character != null, "character is null");
			Assert();

			character.AtbProperties.Subscribe();
			_model.AddCharacter(character);
			_view.AddCharacter(character);
		}

		/// <summary>
		/// Aggiunge una lista di personaggi
		/// </summary>
		/// <param name="characters"></param>
		public void AddCharacters(List<ICombatActor> characters)
		{
			DebugLamplighter.Assert(characters != null, "character is null");
			DebugLamplighter.Assert(characters.Count > 0, "character count is zero");

			foreach(var c in characters)
			{
				AddCharacter(c);
			}
		}

		/// <summary>
		/// Pulisce l'intera lista
		/// </summary>
		public void ClearCharactersInCombat()
		{
			Assert();

			_model.ClearCharacters();
			_view.ClearCharacters();
		}

		/// <summary>
		/// Rimuove un personaggio dalla lista
		/// </summary>
		/// <param name="character"></param>
		public void RemoveCharacterInCombat(ICombatActor character)
		{
			Assert();

			_model.RemoveCharacter(character);
			_view.RemoveCharacter();
		}

		public void StopAtb()
		{
			_inCharging = false;
			ClearCharactersInCombat();
			_currentIndex = 0;
		}

		public override void _ExitTree()
		{
			base._ExitTree();

			Dispose();
		}

		public void Dipose()
		{
			BattleManager.TriggerOnStartCombat -= StartCombat;
			_bag.Build().Dispose();
		}
	}
}
