using System;
using System.Collections.Generic;
using Characters.Interfaces;
using SystemLamplighter.ATB.Interfaces;
using SystemLamplighter.Debug;

namespace SystemLamplighter.ATB
{
	public class AtbService : IAtbService
	{
		bool _inCharging;
		List<ICombatActor> _characters;
		int _currentIndex;
		ICombatActor _currentActorInCommand;
		private IDisposable? _atbCommandEndSubscription;

		public void ClockingAtb(double delta)
		{
			//processa la posizione per ogni personaggio
			// Se lo stato di tutti i personaggi è in charging e ci sono personaggi nell'atb allora il ciclo può continuare
			if (_inCharging && _model.CharactersCount > 0)
			{
				// _currentIndex mantiene l'ultimo indice che è stato controllato
				int i = _currentIndex;
				while (i < _model.CharactersCount)
				{
					var currentCharacter = _model.Characters[i];
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

	}
}