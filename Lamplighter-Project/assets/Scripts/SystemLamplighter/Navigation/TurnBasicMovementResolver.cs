using System.Numerics;

public class TurnBasicMovementResolver
{
	private readonly INavigationAstar _navigation;
    
	// TODO: controllare se questo ora funziona senza problemi dato che dovrà essere passato da Gamebootstrap 
    public TurnBasicMovementResolver(INavigationAstar navigation)
    {
        _navigation = navigation;
    }

    public Godot.Vector3[] ResolveMovement(Godot.Vector3 from, Godot.Vector3 to)
    {
        return _navigation.FindPath(from, to);
    }

    public Godot.Vector3[] ResolveMovementInRange(Godot.Vector3 casterPostion, Godot.Vector3 targetPosition, float range)
    {
        var direction = (casterPostion - targetPosition).Normalized();
        var optimalPoint = targetPosition + direction * range;
        
        return ResolveMovement(casterPostion, optimalPoint);
    }


}