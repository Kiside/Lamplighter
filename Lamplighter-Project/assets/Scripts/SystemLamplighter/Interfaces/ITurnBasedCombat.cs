using System;
using System.Collections.Generic;
using Godot;
using SystemLamplighter.DataStructure.GeneralData;
using SystemLamplighter.Navigation;

namespace SystemLamplighter.Interfaces;

/// <summary>
/// Interfaccia per il combattimento a turno
/// </summary>
public interface ITurnBasedCombat : IDisposable
{
	public void Init(TurnBasedCombatContext turnBasedCombatContext, IMovementService movementService, AnimationPlayer animationPlayer);
	public void ActionChoosedHandler(IActionData actionData);
	//public void OpenBattleSubMenuHandler(ISubMenuDefinition subMenuIds);
	public void HandleAttackAction();
	public void HandleGuardAction();
	public void HandleMagicAction();
	public void HandleItemAction();
	public void HandleEscapeAction();

}