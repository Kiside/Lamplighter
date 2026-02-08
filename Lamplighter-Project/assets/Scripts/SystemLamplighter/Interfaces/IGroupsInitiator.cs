using Godot;

namespace SystemLamplighter.Interfaces;

/// <summary>
/// Interfaccia per l'inizializzazione dei gruppi
/// </summary>
public interface IGroupsInitiator
{
	public void GroupsInit(Node node);
	public void RemoveFromGroups(Node node);

}