using Characters.Interfaces;
using MessagePipe;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.Events;


namespace SystemLamplighter.ATB.Interfaces;

/// <summary>
/// Interfaccia per la logica di business di un personaggio nell'ATB
/// </summary>
public interface IAtbCharacterService
{
	public CharacterProperties CharacterProperties {get;}

	public void Init(CharacterProperties characterProperties, 
	ISubscriber<AtbEndExecuteActionEvent> subscriberAtbEndExecuteActionEvent,
	ISubscriber<AtbCommandPhaseEndEvent> subscribeAtbCommandPhaseEndEvent);
	public void Subscribe();
	public void Unsubscribe();
	public AtbCharacterStatus UpdatePosition(float value); 
	public AtbCharacterStatus CheckPositionStatus();
	public void OnEndCommandStatus(AtbCommandPhaseEndEvent ev);
	public void OnEndAction(AtbEndExecuteActionEvent ev);
	public void Dispose();
}