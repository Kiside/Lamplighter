using System.Collections.Generic;
using Characters.Interfaces;
using Godot;
using SystemLamplighter.Debug;
using SystemLamplighter.Extensions;
using SystemLamplighter.Interfaces;

namespace SystemLamplighter.Target;

[GlobalClass]
public partial class SelectionTargetResolver : TargetResolver
{
	private Camera3D _camera;
	private readonly ICombatActorRegistry _combatActors;
	private readonly ICombatActorPositionProvider<Node3D> _combatActorPosition;
	private ICombatActor _currentActorHighlighted;
	private ITargetData _currentTargetData;
	private int currentTargetSelected;
	private List<ICombatActor> _combatActorsSelected;



	public SelectionTargetResolver() : this(null, null, null) {}

	public SelectionTargetResolver(Camera3D camera, ICombatActorRegistry combatActors, ICombatActorPositionProvider<Node3D> combatActorPosition)
	{
		_camera = camera;
		_combatActors = combatActors;
		_combatActorPosition = combatActorPosition;
	}

	public override void ResolveTargets(IActionData action, ICombatActor casterActor)
	{
		DebugLamplighter.Assert(action != null, "action is null");
		DebugLamplighter.Assert(casterActor != null, "caster actor is null");

		IsActive = true;
		_currentTargetData = action.TargetData;
		_currentActorHighlighted = _combatActors.GetActor(0);
		currentTargetSelected = 0;

		if(_combatActorsSelected.Count > 0)
			_combatActorsSelected.Clear();
	}

	public void HandleInput(bool leftInput, bool rightInput, bool selectInput, bool cancelInput)
	{
		if(leftInput)
			MoveSelection(-1);
		if(rightInput)
			MoveSelection(1);
		if(selectInput)
			Select();
		if(cancelInput)
			Cancel();
	}


	private void MoveSelection(int direction)
	{
		var index = (_combatActors.GetIndex(_currentActorHighlighted) + direction) % _combatActors.Count;

		_currentActorHighlighted = _combatActors.GetActor(index);

		_camera.LookAt(_combatActorPosition.GetPosition(_combatActors.GetActor(index)));
	}

	private void Select()
	{
		currentTargetSelected++;
		_combatActorsSelected.Add(_currentActorHighlighted);

		if(currentTargetSelected >= _currentTargetData.NumberOfTargets)
		{
			
		}
		else
		{
			
		}
	}

	private void Cancel()
	{
		
	}
}