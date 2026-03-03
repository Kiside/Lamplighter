using Godot;

namespace SystemLamplighter.Interfaces;

/// <summary>
/// Interfaccia per aggiungere il nodo in un gruppo in modo automatico
/// </summary>
public interface IGroupsInitiator
{
	public void GroupsInit(Node node);
	public void RemoveFromGroups(Node node);

}