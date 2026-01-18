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
			_publisher.Publish(new CombatStartedEvent(_combatActorRegistry.GetAllActors()));
	}

	public void StartCombat()
	{ 
		_publisher.Publish(new CombatStartedEvent(_combatActorRegistry.GetAllActors()));
	}

	
}