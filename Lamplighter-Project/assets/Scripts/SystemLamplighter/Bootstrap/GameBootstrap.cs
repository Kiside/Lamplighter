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
		base._EnterTree();

		BuildServices();
	}

	public override void _Ready()
    {
        base._Ready();

        InitTargetController();
		InitSelectionTargetRender();
		InitNavitationSystem();
    }

	private void InitTurnBasicMovement()
	{
		var lamplighterMovementNode = _sceneBinder.Bind<LamplighterMovement>();

		DebugLamplighter.Assert(lamplighterMovementNode != null, "lampligterMovementNode is null");

		lamplighterMovementNode.BootstrapInit(Services.GetRequiredService<TurnBasicMovementResolver>());
	}

	private void InitNavitationSystem()
	{
		// var navigationSystem = this.GetNodesOfGroup(GroupsName.navigationSystem)
		// .FirstOrDefault(n => n is NavigationSystem) as NavigationSystem;

		// Ottine il selectionTargetRender dalla scena
		var navigationSystem = _sceneBinder.Bind<NavigationSystem>();

		DebugLamplighter.Assert(navigationSystem != null, "navigationSystem is null");

		navigationSystem.BootstrapInit(Services.GetRequiredService<NavigationAstarService>());
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
		var services = new ServiceCollection();

		// CORE
		services.AddSingleton<IBattleService, BattleService>();
		services.AddSingleton<ICombatActorRegistry, CombatActorRegistry>();
		services.AddSingleton<ICombatActorPositionProvider<Node3D>, CombatActorPosition3DProvider>();

		services.AddSingleton<IHighlightSystem, HighlightSystem>(); 
		services.AddSingleton<ICombatActorHighlightableProvider, CombatActorHighlightableProvider>();

		services.AddSingleton<ITargetableProvider, TargetableProvider>();
		
		services.AddTransient<SelectionTargetResolver>();
		services.AddTransient<ShapeTargetResolver>();

		services.AddSingleton<ITargetResolverFactory, TargetResolverFactory>();

		services.AddSingleton<INavigationAstar ,NavigationAstarService>();

		services.AddTransient<TurnBasicMovementResolver>();

		// Message pipe
		services.AddMessagePipe();

		Services = services.BuildServiceProvider();
	}


	public override void _ExitTree()
	{
		if(Services is IDisposable disposable)
		{
			disposable.Dispose();
		}
	}
	
}