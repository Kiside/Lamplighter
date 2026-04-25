using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SystemLamplighter.Debug;
using SystemLamplighter.Tool;

public class TurnBasedMovementGlobalResolver
{
	private readonly INavigationAstar _navigation;
    public Queue<Godot.Vector3> Movement {get; private set;}

    private Dictionary<Identification, Queue<Godot.Vector3>> MovementDictionary;
    
    public Queue<Godot.Vector3> GetMovement(Identification id)
    {
        if(MovementDictionary.TryGetValue(id, out var value))
            return value;
        else
        {
            Log.PrintWarning($"No element for key {id} in MovementDictionary");
            return new Queue<Godot.Vector3> ();
        }
           
    }
    
	// TODO: controllare se questo ora funziona senza problemi dato che dovrà essere passato da Gamebootstrap 
    public TurnBasedMovementGlobalResolver(INavigationAstar navigation)
    {
        _navigation = navigation;
        Movement = new Queue<Godot.Vector3>();

        MovementDictionary = new Dictionary<Identification, Queue<Godot.Vector3>>();
    }

    public bool InsertIdentification(Identification id) => 
    MovementDictionary.TryAdd(id, new Queue<Godot.Vector3>());

    public Godot.Vector3[] ResolveMovement(Godot.Vector3 from, Godot.Vector3 to, Identification id)
    {
        var movement = _navigation.FindPath(from, to);

        if(MovementDictionary.ContainsKey(id))
            MovementDictionary[id] = new Queue<Godot.Vector3>(movement);
        else
            Log.PrintError($"There is no {id} in MovementDictionary");


        return movement;
    }

    public Godot.Vector3[] ResolveMovementInRange(Godot.Vector3 casterPostion, Godot.Vector3 targetPosition, float range, Identification id)
    {
        var direction = (casterPostion - targetPosition).Normalized();
        var optimalPoint = targetPosition + direction * range;
        
        var movement = ResolveMovement(casterPostion, optimalPoint, id);

        if(MovementDictionary.ContainsKey(id))
            MovementDictionary[id] = new Queue<Godot.Vector3>(movement);
        else
            Log.PrintError($"There is no {id} in MovementDictionary");


        Log.PrintMessage("ResolveMovementInRange");
        return movement;
    }

    


}