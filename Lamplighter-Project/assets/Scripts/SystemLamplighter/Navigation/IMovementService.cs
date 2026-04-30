public interface IMovementService
{
	public Godot.Vector3 Direction {get;}
	public float Speed {get;}
	public void SetTargetPosition(Godot.Vector3 targetPosition);
}