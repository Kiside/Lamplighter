using Godot;
using SystemLamplighter.DataStructure.GeneralData;
using SystemLamplighter.Interfaces;

public class NpcTurnBasedCombat : ITurnBasedCombat
{
	private readonly ITargetableProvider _targetableProvider;

	public NpcTurnBasedCombat(ITargetableProvider targetableProvider)
	{
		_targetableProvider = targetableProvider;
	}

	public void Init(TurnBasedCombatContext turnBasedCombatContext, IMovementService movementService, AnimationPlayer animationPlayer)
	{
		
	}

	public void ActionChoosedHandler(IActionData actionData)
	{
		throw new System.NotImplementedException();
	}

	public void Dispose()
	{
		throw new System.NotImplementedException();
	}

	public void HandleAttackAction()
	{
		throw new System.NotImplementedException();
	}

	public void HandleEscapeAction()
	{
		throw new System.NotImplementedException();
	}

	public void HandleGuardAction()
	{
		throw new System.NotImplementedException();
	}

	public void HandleItemAction()
	{
		throw new System.NotImplementedException();
	}

	public void HandleMagicAction()
	{
		throw new System.NotImplementedException();
	}
}