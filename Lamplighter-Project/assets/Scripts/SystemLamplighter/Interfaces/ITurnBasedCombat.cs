using System;
namespace SystemLamplighter.Interfaces;

/// <summary>
/// Interfaccia per il combattimento a turno
/// </summary>
public interface ITurnBasedCombat : IDisposable
{
	public void ActionChoosedHandler(IActionData actionData);
	public void OpenBattleSubMenuHandler(ISubMenuDefinition subMenuIds);
	public void HandleAttackAction();
	public void HandleGuardAction();
	public void HandleMagicAction();
	public void HandleItemAction();
	public void HandleEscapeAction();

}