using System;
using System.Collections.Generic;
using Characters.Interfaces;
using MessagePipe;
using Microsoft.Extensions.DependencyInjection;
using SystemLamplighter.ATB.Interfaces;
using SystemLamplighter.Debug;
using SystemLamplighter.Events;
using SystemLamplighter.Interfaces;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.Tool;

namespace SystemLamplighter.ATB
{
	/// <summary>
	/// Servizio dell'ATB che gestisce la logica di business
	/// </summary>
	public class AtbService : IAtbService
	{
		#region ICombatActorRegistry
		ICombatActorProvider _combatActorRegistry;
		int _actorsCount => _combatActorRegistry.GetActors().Count;
		#endregion
		List<ICombatActor> _stackCurrentActorsInCommand;
		bool _inCharging;
		int _currentIndex;

		#region Subscription/Disposable
		private ISubscriber<AtbCommandPhaseEndEvent> _subscriberAtbCommandPhaseEnd;
		private ISubscriber<CombatEndEvent> _subscriberCombatEnd; //TODO DA IMPLEMENTARE
		#nullable enable
		private IDisposable? _subscriptionAtbCommandPhaseEnd;
		#nullable disable
		private IPublisher<AtbCommandPhaseStartedEvent> _publishAtbCommandPhaseStartEvent;
		private IPublisher<AtbExecuteActionEvent> _publishAtbExecuteActionEvent;
		private readonly DisposableBagBuilder _bag;
		#endregion


		public AtbService(bool inCharging, ICombatActorProvider combatActorRegistry,
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

			_stackCurrentActorsInCommand = new List<ICombatActor>();

		}

		public void ActivateATB(bool value)
		{
			_inCharging = value;
		}

		public bool ClockingAtb(double delta)
		{
			if (_inCharging && _actorsCount > 0)
			{
				// currentIndex mantiene l'ultimo indiche che è stato controllato
				int i = _currentIndex;
				while (i < _actorsCount)
				{
					var currentCharacter = _combatActorRegistry.GetActor(i);
					if (currentCharacter.AtbStatus == AtbCharacterStatus.CHARGE ||
					currentCharacter.AtbStatus == AtbCharacterStatus.CHARGE_ACTION)
					{
						switch (currentCharacter.UpdateAtbPosition((float)delta))
						{
							// Se un personaggio entra in fase di Command
							case AtbCharacterStatus.COM:
								CommandStatusHandler(currentCharacter, i);
								break;
							// Se un personaggio entra in fase di Action
							case AtbCharacterStatus.ACTION:
								ActionStatusHandler(currentCharacter);
								break;
						}
					}
					i++;
				}
			}
			else if (_inCharging && _actorsCount <= 0)
			{
				Log.PrintMessage("InCharge ma nessun personaggio");
			}
			return _inCharging;
		}

		private void CommandStatusHandler(ICombatActor currentCharacter, int currentIndex)
		{
			if (currentCharacter.AtbProperties.CharacterType == AtbCharacterType.ALLY)
			{
				_inCharging = false;
				_currentIndex = currentIndex;
				_stackCurrentActorsInCommand.Add(currentCharacter);
			}

			Log.PrintMessage($"{currentCharacter.Id.Name} {currentCharacter.Id.ID} COMMAND");

			_subscriptionAtbCommandPhaseEnd = _subscriberAtbCommandPhaseEnd.Subscribe(OnCommandEnd);
			_subscriptionAtbCommandPhaseEnd.AddTo(_bag);
			_publishAtbCommandPhaseStartEvent.Publish(new AtbCommandPhaseStartedEvent(currentCharacter));
		}

		private void ActionStatusHandler(ICombatActor currentCharacter)
		{
			_publishAtbExecuteActionEvent.Publish(new AtbExecuteActionEvent(currentCharacter));
		}

		public void OnCommandEnd(AtbCommandPhaseEndEvent ev)
		{
			DebugLamplighter.Assert(ev != null, "ev is null");

			Log.PrintMessage($"{ev.Actor.AtbProperties.Name} ha finito il command");
			_subscriptionAtbCommandPhaseEnd?.Dispose();
			// TODO: Quello che c'è scritto sotto
			// ! PROBABILMENTE NON MI SERVE PIÙ "_currentActorInCommand" DA CONTROLLARE
			//_currentActorInCommand.AtbProperties.EndCommandStatus(ev.Actor.AtbProperties.SpeedMultiplier);
			_stackCurrentActorsInCommand.Remove(ev.Actor);

			_inCharging = true;
		}

		public void StopAtb()
		{
			_inCharging = false;
			//ClearCharactersInCombat();
			_currentIndex = 0;
		}

		public void Dispose()
		{
			_bag.Build().Dispose();
		}

	}
}