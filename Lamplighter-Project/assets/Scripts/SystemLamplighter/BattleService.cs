using System;
using System.Collections.Generic;
using System.Linq;
using Characters.Interfaces;
using MessagePipe;
using SystemLamplighter;
using SystemLamplighter.Events;

public class BattleService : IBattleService
{
	private readonly IPublisher<CombatStartedEvent> _publisherCombatStarted;
	private readonly IPublisher<CombatEndEvent> _publisherCombatEnded;
	ICombatActorRegistry _combatActorRegistry;

	public BattleService(IPublisher<CombatStartedEvent> publisherCombatStarted, IPublisher<CombatEndEvent>  publisherCombatEnded, ICombatActorRegistry combatActorRegistry)
	{
		_publisherCombatStarted = publisherCombatStarted;
		_publisherCombatEnded = publisherCombatEnded;
		_combatActorRegistry = combatActorRegistry;
	}
	
	public void StartCombat(bool autoStartCombat)
	{
		if(autoStartCombat)
			_publisherCombatStarted.Publish(new CombatStartedEvent(_combatActorRegistry.GetActors()));
	}

	public void StartCombat()
	{ 
		foreach(var a in _combatActorRegistry.GetActors())
		{
			Log.PrintMessage($"actors: {a.AtbProperties.Name}");
		}
		_publisherCombatStarted.Publish(new CombatStartedEvent(_combatActorRegistry.GetActors()));
	}

	public void StopAtb()
	{
		_publisherCombatEnded.Publish(new CombatEndEvent());
	}

	
}