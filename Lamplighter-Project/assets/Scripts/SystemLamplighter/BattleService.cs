using System;
using System.Collections.Generic;
using System.Linq;
using Characters.Interfaces;
using MessagePipe;
using SystemLamplighter;
using SystemLamplighter.Events;

public class BattleService : IBattleService
{
	private readonly IPublisher<CombatStartedEvent> _publisher;
	ICombatActorRegistry _combatActorRegistry;

	public BattleService(IPublisher<CombatStartedEvent> publisher, ICombatActorRegistry combatActorRegistry)
	{
		_publisher = publisher;
		_combatActorRegistry = combatActorRegistry;
	}
	
	public void StartCombat(bool autoStartCombat)
	{
		if(autoStartCombat)
			_publisher.Publish(new CombatStartedEvent(_combatActorRegistry.GetActors()));
	}

	public void StartCombat()
	{ 
		foreach(var a in _combatActorRegistry.GetActors())
		{
			Log.PrintMessage($"actors: {a.AtbProperties.Name}");
		}
		_publisher.Publish(new CombatStartedEvent(_combatActorRegistry.GetActors()));
	}

	
}