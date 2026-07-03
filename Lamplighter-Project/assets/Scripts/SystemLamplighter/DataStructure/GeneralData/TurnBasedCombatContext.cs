using Characters.Interfaces;
using MessagePipe;
using SystemLamplighter.BattleMenu;
using SystemLamplighter.Events;

namespace SystemLamplighter.DataStructure.GeneralData;

/// <summary>
/// Struttura dati per inizializzare il TurnBasedCOmbat
/// </summary>
public class TurnBasedCombatContext
{
	public ICombatBrain CombatBrain { get; private set; }
	public ICombatActor Actor { get; private set; }
	public IPublisher<AtbCommandPhaseEndEvent> PublishCommandPhaseEnd { get; private set; }
	public IPublisher<AtbEndExecuteActionEvent> PublishEndExecuteAction { get; private set; }
	public IPublisher<StartTargetEvent> PublisherStartTarget { get; private set; }
	public ISubscriber<AtbCommandPhaseStartedEvent> SubscriberCommandPhaseStarted { get; private set; }
	public ISubscriber<AtbExecuteActionEvent> SubscriberExecuteAction { get; private set; }
	public ISubscriber<EndTargetEvent> SubscriberEndTarget { get; private set; }

	public TurnBasedCombatContext(ICombatBrain battleMenuController,
		ICombatActor combatActor,
		IPublisher<AtbCommandPhaseEndEvent> publishCommandPhaseEnd,
		IPublisher<AtbEndExecuteActionEvent> publishEndExecuteAction,
		IPublisher<StartTargetEvent> publisherStartTarget,
		ISubscriber<AtbCommandPhaseStartedEvent> subscriberCommandPhaseStarted,
		ISubscriber<AtbExecuteActionEvent> subscriberExecuteAction,
		ISubscriber<EndTargetEvent> subscriberEndTarget)
	{
		CombatBrain = battleMenuController;
		Actor = combatActor;
		PublishCommandPhaseEnd = publishCommandPhaseEnd;
		PublishEndExecuteAction = publishEndExecuteAction;
		PublisherStartTarget = publisherStartTarget;
		SubscriberCommandPhaseStarted = subscriberCommandPhaseStarted;
		SubscriberExecuteAction = subscriberExecuteAction;
		SubscriberEndTarget = subscriberEndTarget;
	}
}