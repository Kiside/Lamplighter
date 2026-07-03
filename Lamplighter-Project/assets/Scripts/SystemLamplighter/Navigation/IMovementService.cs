namespace SystemLamplighter.Navigation;

/// <summary>
/// Interfaccia per il servizio di movimento
/// </summary>
public interface IMovementService
{
	public Godot.Vector3 Direction {get;}
	public float Speed {get;}
	public void SetTargetPosition(Godot.Vector3 targetPosition);
}