using SystemLamplighter.Common.Enums;
using SystemLamplighter.Events;

public interface IAtbCharacterService
{
	public CharacterProperties CharacterProperties {get;}

	public void Init(CharacterProperties characterProperties);
	public void Subscribe();
	public void Unsubscribe();
	public AtbCharacterStatus UpdatePosition(float value); 
	public AtbCharacterStatus CheckPositionStatus();
	public void EndCommandStatus(float speedMultiplier);
	public void OnEndAction(AtbEndExecuteActionEvent ev);
}