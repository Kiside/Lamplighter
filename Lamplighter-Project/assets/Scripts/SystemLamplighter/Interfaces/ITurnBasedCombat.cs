using System;
using System.Collections.Generic;
using SystemLamplighter.DataStructure.GeneralData;
namespace SystemLamplighter.Interfaces;

/// <summary>
/// Interfaccia per il combattimento a turno
/// </summary>
public interface ITurnBasedCombat : IDisposable
{
	Queue<Godot.Vector3> CombatMovement {get;}
	public void Init(TurnBasedCombatContext turnBasedCombatContext);
	public void ActionChoosedHandler(IActionData actionData);
	public void OpenBattleSubMenuHandler(ISubMenuDefinition subMenuIds);
	public void HandleAttackAction();
	public void HandleGuardAction();
	public void HandleMagicAction();
	public void HandleItemAction();
	public void HandleEscapeAction();

}