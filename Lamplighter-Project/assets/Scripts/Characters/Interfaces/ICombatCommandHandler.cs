using SystemLamplighter.Events;

namespace Characters.Interfaces;
/// <summary>
/// Interfaccia per il metodo che gestisce la fase di inizio comando
/// </summary>
public interface ICombatCommandHandler
{
	void OnCommandPhaseStarted(AtbCommandPhaseStartedEvent ev);
}