using SystemLamplighter.Abstract.MVC;
using SystemLamplighter.Interfaces;

namespace SystemLamplighter.Target;

/// <summary>
/// Model per la classe del targetizzazione
/// </summary>
public partial class TargetModel : AbstractModel
{
	public ITargetService _targetService;

	public override void Init()
	{
		
	}
}