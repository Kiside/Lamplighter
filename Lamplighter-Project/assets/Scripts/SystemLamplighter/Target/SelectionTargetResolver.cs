using System;
using System.Collections.Generic;
using Characters.Interfaces;
using Godot;
using Microsoft.Extensions.DependencyInjection;
using SystemLamplighter.Bootstrap;
using SystemLamplighter.Common.Enums;
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
	private List<TargeTableType> _currentWhoTarget => _currentTargetData.WhoTarget;
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
		if(_currentTargetData.TargetType == TargetType.SELF)
		{
			// TODO: Verdere se è possibile non usare il nome per tale get ma un identificativo migliore
			_currentTargetFocused = _targetableProvider.GetTargetable(_casterActor.Id);
			//_currentTargetFocused = _targetableProvider.GetTargetable(_casterActor.AtbProperties.Name);
		}
		_currentTargetFocused = _targetableProvider.GetTargetable(0, action.TargetData.WhoTarget);
		_currentCountTargetsSelected = 0;
		
		if(_targetablesSelected == null)
			_targetablesSelected = new List<ITargetable>();

		if(_targetablesSelected.Count > 0)
			_targetablesSelected.Clear();

		return new ActorCursorState(_currentTargetFocused, _currentTargetData.WhoTarget);
	}

	public override TargetCursorState MoveTarget(Vector2 direction)
	{
		// TODO: Sistemare

		if(_currentTargetData.TargetType == TargetType.SELF)
			return new ActorCursorState(_currentTargetFocused, _currentTargetData.WhoTarget);

		var curTarIndex = _targetableProvider.GetIndex(_currentTargetFocused, _currentWhoTarget);

		Log.PrintMessage($"CurIndex: {curTarIndex} - directionY: {-direction.Y}");

		var i = curTarIndex + (int)Math.Round(-direction.Y);

		var index = i < 0 ? _targetableProvider.CountOf(_currentWhoTarget) - 1 : i % _targetableProvider.CountOf(_currentWhoTarget);
		Log.PrintMessage($"INDEX: {index}");
		_currentTargetFocused = _targetableProvider.GetTargetable(index, _currentWhoTarget);

		

		return new ActorCursorState(_targetablesSelected, _currentTargetFocused, _currentTargetData.WhoTarget);
	}

	public override TargetCursorState Select()
	{
		if(_targetablesSelected.Contains(_currentTargetFocused))
		{
			Log.PrintMessage("REMOVE");
			_currentCountTargetsSelected--;
			_targetablesSelected.Remove(_currentTargetFocused);
			Log.PrintMessage("count targetableSelected: " + _targetablesSelected.Count);
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
			var state = new ActorCursorState(new List<ITargetable>(_targetablesSelected), _currentTargetFocused, new List<TargeTableType>(_currentWhoTarget) ,TargetResolutionStatus.RESOLVED);
			Dispose();
			return state;
		}
		else
		{
			return new ActorCursorState(new List<ITargetable>(_targetablesSelected), _currentTargetFocused, new List<TargeTableType>(_currentWhoTarget) ,TargetResolutionStatus.ON_GOING);
		}
	}

	public override TargetCursorState Cancel()
	{
		return new ActorCursorState(new List<ITargetable>(_targetablesSelected), _currentTargetFocused, new List<TargeTableType>(_currentWhoTarget) ,TargetResolutionStatus.CANCELED);
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