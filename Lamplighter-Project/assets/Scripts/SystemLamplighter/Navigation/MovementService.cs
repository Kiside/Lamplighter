using Godot;

// TODO: CAMBIARE NOME A CLASSE E INTERFACCIA?
public class MovementService : IMovementService
{
	public Godot.Vector3 Direction {get; private set;}
	public float Speed {get; private set;}

	private NavigationAgent3D _navigationAgent;



	public Godot.Vector3 InputMovement(double delta)
	{
		return Godot.Vector3.Back;
	}

	public void SetNavigationAgent(NavigationAgent3D navigationAgent3D) 
	{
		_navigationAgent = navigationAgent3D;

		_navigationAgent.PathDesiredDistance = 0.3f;
		_navigationAgent.TargetDesiredDistance = 2f;
	}

	public void SetTargetPosition(Godot.Vector3 targetPosition) => _navigationAgent.TargetPosition = targetPosition;

	public void SetCalculatedOptmizeTargetPosition(Godot.Vector3 casterPostion, Godot.Vector3 targetPosition, float range)
	{
		var direction = (casterPostion - targetPosition).Normalized();
		var optimalPoint = targetPosition + direction * range;

		SetTargetPosition(optimalPoint);
	}

	public Godot.Vector3 TargetPositionMovement(Godot.Vector3 globalPosition)
	{
		if(_navigationAgent.IsNavigationFinished())
			return Godot.Vector3.Zero;

			
		var destination = _navigationAgent.GetNextPathPosition();
		var res = globalPosition.DirectionTo(destination);
		return res;
	}

	public bool IsNavigationFinished() => _navigationAgent.IsNavigationFinished();
}
