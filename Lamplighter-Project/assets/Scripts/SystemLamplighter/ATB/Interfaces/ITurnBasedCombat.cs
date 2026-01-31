using System;

public interface ITurnBasedCombat : IDisposable
{
	public void ActionChoosedHandler();
	public void OpenBattleSubMenuHandler(ISubMenuDefinition subMenuIds);
	public void HandleAttackAction();
	public void HandleGuardAction();
	public void HandleMagicAction();
	public void HandleItemAction();
	public void HandleEscapeAction();

}