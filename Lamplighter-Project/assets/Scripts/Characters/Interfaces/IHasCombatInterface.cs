namespace Characters.Interfaces;
/// <summary>
/// Interfaccia per il personaggio che ha un'interfaccia di combattimento
/// </summary>
/// <typeparam name="TCombatInterface"></typeparam>
public interface IHasCombatInterface<TCombatInterface>
{
	public TCombatInterface GetCombatInterface();
}