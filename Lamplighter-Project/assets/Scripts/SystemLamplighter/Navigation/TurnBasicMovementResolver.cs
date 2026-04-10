public class TurnBasicMovementResolver
{
	private readonly INavigationAstar _navigation;
    
	// TODO: controllare se questo ora funziona senza problemi dato che dovrà essere passato da Gamebootstrap 
    public TurnBasicMovementResolver(INavigationAstar navigation)
    {
        _navigation = navigation;
    }


}