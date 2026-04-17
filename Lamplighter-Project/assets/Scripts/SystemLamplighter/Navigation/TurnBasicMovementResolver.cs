using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SystemLamplighter.Tool;

public class TurnBasicMovementResolver
{
	private readonly INavigationAstar _navigation;
    public Queue<Godot.Vector3> Movement {get; private set;}
    
	// TODO: controllare se questo ora funziona senza problemi dato che dovrà essere passato da Gamebootstrap 
    public TurnBasicMovementResolver(INavigationAstar navigation)
    {
        _navigation = navigation;
        Movement = new Queue<Godot.Vector3>();
    }

    public Godot.Vector3[] ResolveMovement(Godot.Vector3 from, Godot.Vector3 to)
    {
        var movement = _navigation.FindPath(from, to);
        Movement = new Queue<Godot.Vector3>(movement);
        Log.PrintMessage("ResolveMovement");
        return movement;
    }

    public Godot.Vector3[] ResolveMovementInRange(Godot.Vector3 casterPostion, Godot.Vector3 targetPosition, float range)
    {
        var direction = (casterPostion - targetPosition).Normalized();
        var optimalPoint = targetPosition + direction * range;
        
        var movement = ResolveMovement(casterPostion, optimalPoint);
        Movement = new Queue<Godot.Vector3>(movement);
        Log.PrintMessage("ResolveMovementInRange");
        return movement;
    }

    


}