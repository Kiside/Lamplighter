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
using SystemLamplighter.Tool;

namespace SystemLamplighter.Target;


public class SelectionTargetResolver : TargetResolver
{
	private readonly ITargetableProvider _targetableProvider;

	private ICombatActor _casterActor;
	private ITargetable _currentTargetFocused;
	private ITargetData _currentTargetData;
	private int _currentCountTargetsSelected;
	private List<ITargetable> _targetablesSelected;

	

	public SelectionTargetResolver(ITargetableProvider targetableProvider)
	{
		_targetableProvider = targetableProvider;
	}

	public override TargetCursorState ResolveTargets(IActionData action, ICombatActor casterActor)
	{
		DebugLamplighter.Assert(action != null, "action is null");
		DebugLamplighter.Assert(casterActor != null, "caster actor is null");
		DebugLamplighter.Assert(_targetableProvider != null || _targetableProvider.Count > 0, "_targetableProvider is null or empty");

		IsActive = true;
		_currentTargetData = action.TargetData;
		_casterActor = casterActor;
		_currentTargetFocused = _targetableProvider.GetTargetable(0);
		_currentCountTargetsSelected = 0;
		
		if(_targetablesSelected == null)
			_targetablesSelected = new List<ITargetable>();

		if(_targetablesSelected.Count > 0)
			_targetablesSelected.Clear();

		return new ActorCursorState(_currentTargetFocused);
	}

	public override TargetCursorState MoveTarget(Vector2 direction)
	{
		// TODO: Sistemare


		var curTarIndex = _targetableProvider.GetIndex(_currentTargetFocused);

		var i = curTarIndex + (int)Math.Round(-direction.Y);

		var index = i < 0 ? _targetableProvider.Count - 1 : i % _targetableProvider.Count;
		
		_currentTargetFocused = _targetableProvider.GetTargetable(index);

		Log.PrintMessage($"TARGET FOCUSED: {_currentTargetFocused.TargetableName} - INDEX: {index}");

		return new ActorCursorState(_currentTargetFocused);
	}

	public override TargetCursorState Select()
	{
		if(_targetablesSelected.Contains(_currentTargetFocused))
		{
			_currentCountTargetsSelected--;
			_targetablesSelected.Remove(_currentTargetFocused);
		}
		else
		{
			_currentCountTargetsSelected++;
			_targetablesSelected.Add(_currentTargetFocused);
		}

		return CheckResolution();
	}

	private TargetCursorState CheckResolution()
	{
		if(_currentCountTargetsSelected >= _currentTargetData.NumberOfTargets)
		{
			var state = new ActorCursorState(_targetablesSelected, _currentTargetFocused, TargetResolutionStatus.RESOLVED);
			Dispose();
			return state;
		}
		else
		{
			return new ActorCursorState(_targetablesSelected, _currentTargetFocused, TargetResolutionStatus.ON_GOING);
		}
	}

	public override TargetCursorState Cancel()
	{
		return new ActorCursorState(_targetablesSelected, _currentTargetFocused, TargetResolutionStatus.CANCELED);
	}

	public override void Dispose()
	{
		_targetablesSelected.Clear();
		_currentTargetFocused = null;
		_currentTargetData = null;
		_currentCountTargetsSelected = 0;
		_casterActor = null;
	}
}