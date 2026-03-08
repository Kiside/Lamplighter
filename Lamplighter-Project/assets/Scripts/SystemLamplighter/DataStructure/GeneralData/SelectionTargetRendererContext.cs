using Godot;
using SystemLamplighter.Interfaces;

public class SelectionTargetRendererContext
{
	public ICombatActorHighlightableProvider highlightableProvider {get; init;}
	public ICombatActorPositionProvider<Node3D> combatActorPositionProvider {get; init;}
	public IHighlightSystem highlightSystem {get; init;}
}