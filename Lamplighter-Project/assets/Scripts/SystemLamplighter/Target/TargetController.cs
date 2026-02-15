using SystemLamplighter.Abstract.MVC;
using SystemLamplighter.Interfaces;
using SystemLamplighter.Extensions;
using SystemLamplighter.Debug;

namespace SystemLamplighter.Target;

/// <summary>
/// Controller per la classe che si occupa della logica della targetizzazione
/// </summary>
public partial class TargetController : AbstractController<TargetView, TargetModel>
{
	
	

	public override void _Ready()
	{
		base._Ready();
	}

	public override void Init()
	{
		base.Init();

		this.SubscribeEvent<StartTargetEvent>(OnStartTarget);
	}

	private void OnStartTarget(StartTargetEvent ev)
	{
		DebugLamplighter.Assert(ev != null, "ev is null");
		DebugLamplighter.Assert(ev.Action != null, "action is null");
		DebugLamplighter.Assert(ev.Actor != null, "actor is null");

		var targetType = ev.Action.TargetData.TargetType;

		if(targetType == Common.Enums.TargetType.SINGLE || targetType == Common.Enums.TargetType.GROUP)
		{
			_model.CurrentTargetResolver = _model.TargetResolvers.Find(r => r is SelectionTargetResolver);
		}
		else
		{
			_model.CurrentTargetResolver = _model.TargetResolvers.Find(r => r is ShapeTargetResolver);
		}

		_model.CurrentTargetResolver.ResolveTargets(ev.Action, ev.Actor);
	}

	public override void _ExitTree()
	{
		//_targetService.Dispose();
	
		base._ExitTree();
	}
}