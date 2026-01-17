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
	private List<ICombatActor> _actors;
	private ICombatQue

	public BattleService(IPublisher<CombatStartedEvent> publisher)
	{
		_publisher = publisher;
	}

	public void SetActors(IReadOnlyList<ICombatActor> actors)
	{
		_actors = actors.ToList();
	}
	
	public void StartCombat(bool autoStartCombat)
	{
		if(autoStartCombat)
			_publisher.Publish(new CombatStartedEvent(_actors));
	}

	public void StartCombat()
	{ 
		_publisher.Publish(new CombatStartedEvent(_actors));
	}

	
}