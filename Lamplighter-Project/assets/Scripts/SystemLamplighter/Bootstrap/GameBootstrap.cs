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
using Characters;
using SystemLamplighter.Abstract.MVC;
using Characters.NPC;


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
		
		InitTargetController();
		InitSelectionTargetRender();
		InitCombatAndMovementCharacter();
		base._Ready();
	}

	public void InitCharacters()
	{
		var characterNodes = _sceneBinder.BindAll<LamplighterCharacterController>();

		DebugLamplighter.Assert(characterNodes != null, "playableCharacterNodes is null");

		foreach(var character in characterNodes)
		{
			character.BootstrapInit(Services.GetRequiredService<IEffectResolver>());
		}
	}

	private void InitCombatAndMovementCharacter()
	{
		var playablecharacterNodes = _sceneBinder.BindAll<LamplighterCharacterModel>();
		//var noPlayableCharacterNodes = _sceneBinder.BindAll<NpCharacterModel>();

		var movementService = Services.GetRequiredService<IMovementService>();

		DebugLamplighter.Assert(playablecharacterNodes != null, "playableCharacterNodes is null");

		// TODO : IL GLOBAL MOVMENT RESOLVER È UN SINGLETEON, BISOGNA FARE IN MODO CHE OGNI COMBAT NODE GLI DIA IL PROPRIO IDENTIFICATIVO

		// Foreach Playable
		foreach(var playableCharacter in playablecharacterNodes)
		{
			InitMovementService(playableCharacter.Combat as LamplighterCombat, 
			playableCharacter.Movement as LamplighterMovement, 
			movementService);
		}

		// FOREACH NO PLAYABLE CHARACTER
		// foreach(var noPlayableCharacter in noPlayableCharacterNodes)
		// {
		// 	InitGlobalMovementResolverIntoCharacters(noPlayableCharacter.Combat as LamplighterCombat, noPlayableCharacter.Movement as LamplighterMovement, globalMovementResolver);
		// }
	}

	private void InitMovementService(LamplighterCombat combatNode, LamplighterMovement movementNode, IMovementService movementService)
	{
		combatNode.BootstrapInit(Services.GetRequiredService<ITurnBasedCombatFactory>(), movementService);

		movementNode.BootstrapInit(movementService);
	}

	

	private void InitSelectionTargetRender()
	{
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
		// todo BATTLE SERVICE DA CANCELLARE 
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

		services.AddTransient<IMovementService, MovementService>();
		
		services.AddSingleton<ITurnBasedCombatFactory, TurnBasedCombatFactory>();
		Log.PrintMessage("ITurnBasedCombatFactory");

		services.AddTransient<NpcTurnBasedCombat>();
		services.AddTransient<TurnBasedCombat>();
		Log.PrintMessage("ITurnBasedCombat");

		services.AddTransient<IEffectResolver, EffectResolver>();
		Log.PrintMessage("IEffectResolver");

		// Message pipe
		services.AddMessagePipe();

		Services = services.BuildServiceProvider();

		Log.PrintMessage("Services Builded");
	}


	public override void _ExitTree()
	{
		if(Services is IDisposable disposable)
		{
			disposable.Dispose();
		}
	}
	
}
