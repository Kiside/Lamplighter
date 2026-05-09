using SystemLamplighter.Interfaces;

public interface ICombatBrain
{
	public void TurnOn();
	public void TurnOff();
	public void Action(IActionData actionData); 
}