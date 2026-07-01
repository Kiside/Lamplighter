using SystemLamplighter.Interfaces;

/// <summary>
/// Interfaccia per le classi che si occupano della scelta dei personaggi nel combattimento
/// </summary>
public interface ICombatBrain
{
	public void TurnOn();
	public void TurnOff();
	public void Action(IActionData actionData); 
}