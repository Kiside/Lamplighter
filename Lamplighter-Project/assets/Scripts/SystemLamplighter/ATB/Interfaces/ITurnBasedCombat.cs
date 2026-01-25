using System;

public interface ITurnBasedCombat
{
	public void ActionChoosedHandler();
	public void OpenBattleSubMenuHandler(int subMenuIds);
	public void HandleAttackAction();
	public void HandleGuardAction();
	public void HandleMagicAction();
	public void HandleItemAction();
	public void HandleEscapeAction();
}