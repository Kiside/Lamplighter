using System;
using System.Collections.Generic;
using Characters.Interfaces;
using MessagePipe;
using Microsoft.Extensions.DependencyInjection;
using SystemLamplighter.ATB.Interfaces;
using SystemLamplighter.Debug;
using SystemLamplighter.Events;

namespace SystemLamplighter.ATB
{
	public class AtbService : IAtbService
	{
		#region ICombatActorRegistry
		ICombatActorRegistry _combatActorRegistry;
		int _actorsCount => _combatActorRegistry.GetActors().Count;
		#endregion
		ICombatActor _currentActorInCommand;
		bool _inCharging;
		int _currentIndex;

		#region Subscription/Disposable
		private ISubscriber<AtbCommandPhaseEndEvent> _subscriberAtbCommandPhaseEnd;
		private ISubscriber<CombatEndEvent> _subscriberCombatEnd;
		#nullable enable
		private IDisposable? _subscriptionAtbCommandPhaseEnd;
		#nullable disable
		private IPublisher<AtbCommandPhaseStartedEvent> _publishAtbCommandPhaseStartEvent;
		private IPublisher<AtbExecuteActionEvent> _publishAtbExecuteActionEvent;
		private readonly DisposableBagBuilder _bag;
		#endregion
		

		public AtbService(bool inCharging, ICombatActorRegistry combatActorRegistry, 
		ISubscriber<AtbCommandPhaseEndEvent> atbCommandPhaseEndSubscriber,
		ISubscriber<CombatEndEvent> subscriberCombatEnd,
		IPublisher<AtbCommandPhaseStartedEvent> publishAtbCommandPhaseStartEvent,
		IPublisher<AtbExecuteActionEvent> publishAtbExecuteActionEvent)
		{
			_bag = DisposableBag.CreateBuilder();
			_inCharging = inCharging;
			_combatActorRegistry = combatActorRegistry;
			_currentIndex = 0;
			_subscriberAtbCommandPhaseEnd = atbCommandPhaseEndSubscriber;
			_subscriberCombatEnd = subscriberCombatEnd;
			_publishAtbCommandPhaseStartEvent = publishAtbCommandPhaseStartEvent;
			_publishAtbExecuteActionEvent = publishAtbExecuteActionEvent;

		}

		public void ActivateATB(bool value)
		{
			_inCharging = value;
		}

		public bool ClockingAtb(double delta)
		{
			if(_inCharging && _actorsCount > 0)
			{
				// currentIndex mantiene l'ultimo indiche che è stato controllato
				int i = _currentIndex;
				while(i < _actorsCount)
				{
					var currentCharacter = _combatActorRegistry.GetActor(i);
					if(currentCharacter.AtbStatus == AtbCharacterStatus.CHARGE ||
					currentCharacter.AtbStatus == AtbCharacterStatus.CHARGE_ACTION)
					{
						switch(currentCharacter.UpdateAtbPosition((float)delta))
						{
							// Se un personaggio entra in fase di Command
							case AtbCharacterStatus.COM:
							// Bisogna evitare il continuo del ciclo e fermare il proseguimento dell'ATB
							_inCharging = false;
							_currentIndex = i;
							_currentActorInCommand = currentCharacter;
							_subscriptionAtbCommandPhaseEnd = _subscriberAtbCommandPhaseEnd.Subscribe(OnCommandEnd);
							_subscriptionAtbCommandPhaseEnd.AddTo(_bag);
							_publishAtbCommandPhaseStartEvent.Publish(new AtbCommandPhaseStartedEvent(currentCharacter));
							break;
							case AtbCharacterStatus.ACTION:
							_publishAtbExecuteActionEvent.Publish(new AtbExecuteActionEvent(currentCharacter));
							break;
						}
					}
					//_view.UpdatePosition(_model.Characters[i].AtbProperties.Position, i);
					i++;
				}
			}
			else if(_inCharging && _actorsCount <= 0)
			{
				Log.PrintMessage("InCharge ma nessun personaggio");
			}
			return _inCharging;
		}


		public void StartCombat(CombatStartedEvent ev)
		{
			// Addcharacters?
			ActivateATB(true);
		}

		public void OnCommandEnd(AtbCommandPhaseEndEvent ev)
		{
			DebugLamplighter.Assert(ev != null, "ev is null");
			DebugLamplighter.Assert(_currentActorInCommand != null, "_currentActorInCommand is null");

			_subscriptionAtbCommandPhaseEnd?.Dispose();
			// ! PROBABILMENTE NON MI SERVE PIÙ "_currentActorInCommand" DA CONTROLLARE
			_currentActorInCommand = null;

			_inCharging = true;
			
		}

		public void Dispose()
		{
			_bag.Build().Dispose();
		}


		public void OLDClockingAtb(double delta)
		{
			//processa la posizione per ogni personaggio
			// Se lo stato di tutti i personaggi è in charging e ci sono personaggi nell'atb allora il ciclo può continuare
			// if (_inCharging && _model.CharactersCount > 0)
			// {
			// 	// _currentIndex mantiene l'ultimo indice che è stato controllato
			// 	int i = _currentIndex;
			// 	while (i < _model.CharactersCount)
			// 	{
			// 		var currentCharacter = _model.Characters[i];
			// 		if(currentCharacter.AtbProperties.Status == AtbCharacterStatus.CHARGE || currentCharacter.AtbProperties.Status == AtbCharacterStatus.CHARGE_ACTION)
			// 		{
			// 			switch(currentCharacter.AtbProperties.UpdatePosition((float)delta))
			// 			{
			// 				// Se un personaggio entra in Command
			// 				case AtbCharacterStatus.COM:
			// 				// Bisogna evitare il continuo del ciclo è "fermare" il proseguimento dell'ATB
			// 				_inCharging = false;
			// 				_currentIndex = i;
			// 				_currentActorInCommand = currentCharacter;
			// 				_atbCommandEndSubscription = this.SubscribeEvent<AtbCommandPhaseEndEvent>(OnCommandEnd);
			// 				_atbCommandEndSubscription.AddTo(_bag);
			// 				this.PublishEvent(new AtbCommandPhaseStartedEvent(currentCharacter));
			// 				break;
			// 				// In stato Action
			// 				case AtbCharacterStatus.ACTION:
			// 				// Bisogna chiamare l'eseguimento dell'azione
			// 				this.PublishEvent(new AtbExecuteActionEvent(currentCharacter));
			// 				break;
			// 			}
			// 		}
			// 			//Chiedo alla view di riposizionare i vari personaggi
			// 			_view.UpdatePosition(_model.Characters[i].AtbProperties.Position, i);
			// 			i++;
			// 	}
			// 	_currentIndex = 0;
			// }
			// else if (_inCharging && _model.CharactersCount <= 0)
			// {
			// 	Log.PrintMessage("InCharge ma nessun personaggio");
			// }
		}

	}
}