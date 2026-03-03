using System;
using System.Collections.Generic;
using Characters.Interfaces;
using Godot;
using Microsoft.Extensions.DependencyInjection;
using SystemLamplighter.Bootstrap;
using SystemLamplighter.DataStructure.GeneralData;
using SystemLamplighter.Debug;
using SystemLamplighter.Extensions;
using SystemLamplighter.Interfaces;

namespace SystemLamplighter.Target;


public class SelectionTargetResolver : TargetResolver
{
	private readonly ICombatActorPositionProvider<Node3D> _combatActorPosition;

	private ICombatActor _casterActor;
	private ICombatActor _currentActorHighlighted;
	private ITargetData _currentTargetData;
	private int currentTargetSelected;
	private List<ICombatActor> _combatActorsSelected;

	

	public SelectionTargetResolver(ICombatActorPositionProvider<Node3D> combatActorPosition)
	{
		_combatActorPosition = combatActorPosition;
	}

	public override TargetCursorState ResolveTargets(IActionData action, ICombatActor casterActor)
	{
		DebugLamplighter.Assert(action != null, "action is null");
		DebugLamplighter.Assert(casterActor != null, "caster actor is null");
		DebugLamplighter.Assert(_combatActorPosition != null || _combatActorPosition.Count > 0, "_combatActorPosition is null or empty");

		IsActive = true;
		_currentTargetData = action.TargetData;
		_casterActor = casterActor;
		_currentActorHighlighted = _combatActorPosition.GetActor(0);
		currentTargetSelected = 0;
		
		if(_combatActorsSelected == null)
			_combatActorsSelected = new List<ICombatActor>();

		if(_combatActorsSelected.Count > 0)
			_combatActorsSelected.Clear();

		return new ActorCursorState(_currentActorHighlighted);
	}

	public override TargetCursorState MoveTarget(Vector2 direction)
	{
		var index = (_combatActorPosition.GetIndex(_currentActorHighlighted) + (int)Math.Round(direction.Y)) % _combatActorPosition.Count;

		_currentActorHighlighted = _combatActorPosition.GetActor(index);

		return new ActorCursorState(_currentActorHighlighted);
	}

	public override TargetResolutionData Select()
	{
		currentTargetSelected++;
		_combatActorsSelected.Add(_currentActorHighlighted);

		if(currentTargetSelected >= _currentTargetData.NumberOfTargets)
		{
			return new TargetResolutionData(
				new TargetResolutionContext(_combatActorPosition.GetPositionsFromActors(_combatActorsSelected), _casterActor),
				TargetResolutionStatus.RESOLVED
			);
		}
		else
		{
			return new TargetResolutionData(
				new TargetResolutionContext(),
				TargetResolutionStatus.ON_GOING
			);
		}
	}

	public override TargetResolutionData Cancel()
	{
		return new TargetResolutionData(
				new TargetResolutionContext(),
				TargetResolutionStatus.CANCELED
			);
	}

	public override void Dispose()
	{
		
	}
}