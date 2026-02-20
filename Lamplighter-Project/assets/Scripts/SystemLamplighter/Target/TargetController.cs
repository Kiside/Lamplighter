using SystemLamplighter.Abstract.MVC;
using SystemLamplighter.Interfaces;
using SystemLamplighter.Extensions;
using SystemLamplighter.Debug;
using Godot;
using SystemLamplighter.Common.Input;

namespace SystemLamplighter.Target;

/// <summary>
/// Controller per la classe che si occupa della logica della targetizzazione
/// </summary>
public partial class TargetController : AbstractController<TargetView, TargetModel>
{
	TargetResolver CurrentTargetResolver { get => _model.CurrentTargetResolver; set => _model.CurrentTargetResolver = value; } 
	

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
			CurrentTargetResolver = _model.FindTargetResolver<SelectionTargetResolver>();
		}
		else
		{
			CurrentTargetResolver = _model.FindTargetResolver<ShapeTargetResolver>();
		}

		CurrentTargetResolver.ResolveTargets(ev.Action, ev.Actor);
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		TargetMovement();
		TargetInputHandler();
	}

	private void TargetMovement()
	{
		var cursorState = CurrentTargetResolver?.MoveTarget
		(
			Input.GetVector(LamplighterInputMap.Left, 
			LamplighterInputMap.Right, 
			LamplighterInputMap.Down, 
			LamplighterInputMap.Up)
		);

		if(cursorState != null)
			_view.Render(cursorState);
	}

	private void TargetInputHandler()
	{
		if(Input.IsActionJustPressed(LamplighterInputMap.Select))
			CurrentTargetResolver?.Select();
		if(Input.IsActionJustPressed(LamplighterInputMap.Back))
			CurrentTargetResolver?.Cancel();
	}

	public override void _ExitTree()
	{
		//_targetService.Dispose();
	
		base._ExitTree();
	}
}