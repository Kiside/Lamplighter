using Characters.Interfaces;
using Godot;
using SystemLamplighter.Interfaces;

namespace SystemLamplighter.Target;

public partial class SelectionTargetResolver : TargetResolver
{
	private Camera3D _camera;
	private readonly ICombatActorRegistry _combatActors;
	private readonly ICombatActorPositionProvider<Node3D> _combatActorPosition;
	private ICombatActor _currentActorHighlighted;

	public override void ResolveTargets(IActionData action, ICombatActor mainActor)
	{
		IsActive = true;
	}

	public void HandleInput(bool leftInput, bool rightInput, bool selectInput)
	{
		if(leftInput)
			MoveSelection(-1);
		if(rightInput)
			MoveSelection(1);
		if(selectInput)
			Select();
	}

	private void MoveSelection(int direction)
	{
		var index = (_combatActors.GetIndex(_currentActorHighlighted) + direction) % _combatActors.Count;

		_camera.LookAt(_combatActorPosition.GetPosition(_combatActors.GetActor(index)));
	}

	private void Select()
	{
		
	}
}