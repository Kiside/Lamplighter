using SystemLamplighter.Abstract.MVC;
using SystemLamplighter.Interfaces;
using SystemLamplighter.Extensions;
using SystemLamplighter.Debug;
using Godot;
using SystemLamplighter.Common.Input;
using SystemLamplighter.DataStructure.GeneralData;
using SystemLamplighter.Target.Interfaces;
using SystemLamplighter.Tool;
using System.Collections.Generic;
using SystemLamplighter.Setup;

namespace SystemLamplighter.Target;

/// <summary>
/// Controller per la classe che si occupa della logica della targetizzazione
/// </summary>
public partial class TargetController : AbstractController<TargetView, TargetModel>
{
	#nullable enable
	ITargetResolver? CurrentTargetResolver { get => _model.CurrentTargetResolver; set => _model.CurrentTargetResolver = value; } 
	#nullable disable

	private ITargetResolverFactory _targetResolverFactory;
	
	public override void Init()
	{
		base.Init();
		this.SubscribeEvent<StartTargetEvent>(OnStartTarget);
	}

	public void BootstrapInit(ITargetResolverFactory targetResolverFactory)
	{
		_targetResolverFactory = targetResolverFactory;
	}

	private void OnStartTarget(StartTargetEvent ev)
	{
		DebugLamplighter.Assert(ev != null, "ev is null");
		DebugLamplighter.Assert(ev.Action != null, "action is null");
		DebugLamplighter.Assert(ev.Actor != null, "actor is null");

		var targetType = ev.Action.TargetData.TargetType;

		CurrentTargetResolver = _targetResolverFactory.Create(targetType);

		RenderIfAny(CurrentTargetResolver.ResolveTargets(ev.Action, ev.Actor));
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		TargetMovement();
		TargetInputHandler();
	}

	private void TargetMovement()
	{
		RenderIfAny(CurrentTargetResolver?.MoveTarget
		(
			Input.GetVector(LamplighterInputMap.Left, 
			LamplighterInputMap.Right, 
			LamplighterInputMap.Down, 
			LamplighterInputMap.Up)
		));
		
	}

	private void TargetInputHandler()
	{
		if(Input.IsActionJustPressed(LamplighterInputMap.Select))
			CurrentTargetResolver?.Select();
		if(Input.IsActionJustPressed(LamplighterInputMap.Back))
			CurrentTargetResolver?.Cancel();
	}

	private void RenderIfAny(TargetCursorState cursorState)
	{
		if (cursorState == null)
			return;

		switch (cursorState)
		{
			case PositionCursorState positionCursorState:
				_view.ShapeTargetRender(positionCursorState);
				break;
			case ActorCursorState actorCursorState:
				_view.SelectionTargetRender(actorCursorState);
				break;
		}
	}

	public override void _ExitTree()
	{
		//_targetService.Dispose();
	
		base._ExitTree();
	}
}