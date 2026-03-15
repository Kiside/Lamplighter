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
using Characters.Interfaces;

namespace SystemLamplighter.Target;

/// <summary>
/// Controller per la classe che si occupa della logica della targetizzazione
/// </summary>
public partial class TargetController : AbstractController<TargetView, TargetModel>
{
	#nullable enable
	ITargetResolver? CurrentTargetResolver { get => _model.CurrentTargetResolver; set => _model.CurrentTargetResolver = value; } 
	#nullable disable

	/// <summary>
	/// Salvo il target che è in questo momento il caster
	/// </summary>
	private ICombatActor _currentActorCaster;

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
		
		_currentActorCaster = ev.Actor;
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
		int y = 0;
		if(Input.IsActionJustReleased(LamplighterInputMap.Up))
			y = 1;
		else if (Input.IsActionJustReleased(LamplighterInputMap.Down))
			y = -1;

		var inputVector = new Vector2(0, y);
		if(inputVector != Vector2.Zero)
			RenderIfAny(CurrentTargetResolver?.MoveTarget(inputVector));
		
	}

	private void TargetInputHandler()
	{
		TargetInputSelect();
		TargetInputCancel();
	}

	private void TargetInputSelect()
	{
		if(Input.IsActionJustPressed(LamplighterInputMap.Select))
		{
			var state = CurrentTargetResolver?.Select();

			DebugLamplighter.Assert(state != null, "resolutionData is null");
			DebugLamplighter.Assert(state.TargetResolutionStatus != TargetResolutionStatus.CANCELED, "resolutionData.TargetResolutionStatus is CANCELED WHEN IT CAN'T BE");

			switch(state.TargetResolutionStatus)
			{
				case TargetResolutionStatus.RESOLVED:
					switch(state)
					{
						case PositionCursorState positionCursorState:
						break;
						case ActorCursorState actorCursorState:
						// TODO: Probabilmente in ResolutionContext dovrà andare un TargetCursorState
						var resolutionData = new TargetResolutionContext(actorCursorState.TargetablesSelected, _currentActorCaster);
						Log.PrintMessage("Target risolto, chiamare evento");
						// TODO: da continuare per l'evento di fine target
						break;
					}

					break;
				case TargetResolutionStatus.ON_GOING:
					Log.PrintMessage("Target ONGOING");
					RenderIfAny(state);
					break;
			}
		}
			

	}

	private void TargetInputCancel()
	{
		if(Input.IsActionJustPressed(LamplighterInputMap.Back))
		{
			var resolutionData = CurrentTargetResolver?.Cancel();

			DebugLamplighter.Assert(resolutionData != null, "resolutionData is null");
			DebugLamplighter.Assert(resolutionData.TargetResolutionStatus == TargetResolutionStatus.CANCELED, "resolutionData.TargetResolutionStatus is NOT CANCELED WHEN IT SHOULD BE");


			// TODO: Target risolto, chiamare evento
		}
			
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