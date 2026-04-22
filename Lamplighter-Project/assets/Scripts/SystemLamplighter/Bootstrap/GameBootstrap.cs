using Godot;
using SystemLamplighter;
using Microsoft.Extensions.DependencyInjection;
using MessagePipe;
using System;
using SystemLamplighter.Interfaces;
using SystemLamplighter.Combat.Core;
using SystemLamplighter.Combat.Actor;
using SystemLamplighter.Target;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.Setup;
using System.Collections.Generic;
using System.Linq;
using SystemLamplighter.Debug;
using SystemLamplighter.Extensions;
using SystemLamplighter.DataStructure.GeneralData;
using SystemLamplighter.Navigation;
using Characters.Playable;
using SystemLamplighter.Tool;

namespace SystemLamplighter.Bootstrap;

public partial class GameBootstrap : Node
{
	/// <summary>
	/// nodi che deve trovare il gamebootstrap
	/// </summary>
	[Export]
	private SceneBinder _sceneBinder;



	public static IServiceProvider Services {get; private set;}
	public override void _EnterTree()
	{
		Log.PrintMessage("ENTER TREE:");
		BuildServices();
		base._EnterTree();
	}

	public override void _Ready()
	{
		Log.PrintMessage("READY:");
		InitNavitationSystem();
		InitTargetController();
		InitSelectionTargetRender();
		InitCombat();
		InitTurnBasicMovement();
		base._Ready();
	}

	private void InitCombat()
	{
		var lamplighterCombatNodes = _sceneBinder.BindAll<LamplighterCombat>();

		DebugLamplighter.Assert(lamplighterCombatNodes != null, "lamplighterCombatNode is null");

		foreach(var combatNode in lamplighterCombatNodes)
		{
			combatNode.BootstrapInit(Services.GetRequiredService<ITurnBasedCombat>());
		}
	}

	private void InitTurnBasicMovement()
	{
		var lamplighterMovementNodes = _sceneBinder.BindAll<LamplighterMovement>();

		DebugLamplighter.Assert(lamplighterMovementNodes != null, "lampligterMovementNode is null");

		foreach(var movementNode in lamplighterMovementNodes)
		{
			
			movementNode.BootstrapInit(Services.GetRequiredService<TurnBasicMovementResolver>());
		}
		
	}

	private void InitNavitationSystem()
	{
		// var navigationSystem = this.GetNodesOfGroup(GroupsName.navigationSystem)
		// .FirstOrDefault(n => n is NavigationSystem) as NavigationSystem;

		// Ottine il selectionTargetRender dalla scena
		Log.PrintMessage("navigation system");
		var navigationSystem = _sceneBinder.Bind<NavigationSystem>();
		Log.PrintMessage("after binding navigation system");

		DebugLamplighter.Assert(navigationSystem != null, "navigationSystem is null");

		Log.PrintMessage("bootstrap navigation");
		navigationSystem.BootstrapInit(Services.GetRequiredService<INavigationAstar>());
		Log.PrintMessage("after bootstrap navigation");
	}

	private void InitSelectionTargetRender()
	{
		// var selectionTargetRender = this.GetNodesOfGroup(GroupsName.renderer)
		// .FirstOrDefault(n => n is SelectionTargetRenderer) as SelectionTargetRenderer;

		// Ottine il selectionTargetRender dalla scena
		var selectionTargetRender = _sceneBinder.Bind<SelectionTargetRenderer>();

		DebugLamplighter.Assert(selectionTargetRender != null, "selectionTargetRender is null");

		var context = new SelectionTargetRendererContext
		{
			TargetableProvider = Services.GetRequiredService<ITargetableProvider>(),
		};

		selectionTargetRender?.BootstrapInit(context);
	}

	private void InitTargetController()
	{
		
		// var targetController = this.GetNodesOfGroups(_getNodesOfGroups)
		// 	.FirstOrDefault(n => n is TargetController) as TargetController;

		// Ottieni il nodo TargetController dalla scena
		var targetController = _sceneBinder.Bind<TargetController>();

		DebugLamplighter.Assert(targetController != null, "targetController is null");
		
		// Risolvi la factory dal container DI
		var factory = Services.GetRequiredService<ITargetResolverFactory>();
		// Iniettala nel TargetController
		targetController.BootstrapInit(factory);
	}

	private void BuildServices()
	{
		Log.PrintMessage("building");
		var services = new ServiceCollection();

		// CORE
		services.AddSingleton<IBattleService, BattleService>();
		services.AddSingleton<ICombatActorRegistry, CombatActorRegistry>();
		services.AddSingleton<ICombatActorPositionProvider<Node3D>, CombatActorPosition3DProvider>();
		Log.PrintMessage("BattleService - CombatActorRegistry - CombatActorPosition3DProvider");

		services.AddSingleton<IHighlightSystem, HighlightSystem>(); 
		services.AddSingleton<ICombatActorHighlightableProvider, CombatActorHighlightableProvider>();
		Log.PrintMessage("HighlightSystem - CombatActorHighlightableProvider");

		services.AddSingleton<ITargetableProvider, TargetableProvider>();
		Log.PrintMessage("TargetableProvider");
		
		services.AddTransient<SelectionTargetResolver>();
		services.AddTransient<ShapeTargetResolver>();
		Log.PrintMessage("SelectionTargetResolver - ShapeTargetResolver");

		services.AddSingleton<ITargetResolverFactory, TargetResolverFactory>();
		Log.PrintMessage("ITargetResolverFactory");

		services.AddSingleton<INavigationAstar ,NavigationAstarService>();
		Log.PrintMessage("INavigationAstar");

		services.AddTransient<TurnBasicMovementResolver>();
		Log.PrintMessage("TurnBasicMovementResolver");
		
		services.AddTransient<ITurnBasedCombat, TurnBasedCombat>();
		Log.PrintMessage("ITurnBasedCombat");

		// Message pipe
		services.AddMessagePipe();

		Services = services.BuildServiceProvider();

		Log.PrintMessage("Services Builded");
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		if(Input.IsActionJustReleased("debug"))
			Log.PrintMessage($"Movement now: {Services.GetRequiredService<TurnBasicMovementResolver>().Movement.Count}");
	}


	public override void _ExitTree()
	{
		if(Services is IDisposable disposable)
		{
			disposable.Dispose();
		}
	}
	
}
