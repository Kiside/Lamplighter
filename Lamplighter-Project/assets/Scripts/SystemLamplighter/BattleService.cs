using System;
using System.Collections.Generic;
using Characters.Interfaces;
using MessagePipe;
using SystemLamplighter.Events;

public class BattleService : IBattleService
{
	private readonly IPublisher<CombatStartedEvent> _publisher;

	public BattleService(IPublisher<CombatStartedEvent> publisher)
	{
		_publisher = publisher;
	}
	
	public void StartCombat(IReadOnlyList<ICombatActor> actors, bool autoStartCombat)
	{
		if(autoStartCombat)
			_publisher.Publish(new CombatStartedEvent(actors));
	}

	public void StartCombat(IReadOnlyList<ICombatActor> actors) => _publisher.Publish(new CombatStartedEvent(actors));
}