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

namespace SystemLamplighter.Bootstrap;

public partial class GameBootstrap : Node
{
	/// <summary>
	/// nodi che deve trovare il gamebootstrap
	/// </summary>
	[Export]
	private Godot.Collections.Array<GroupsName> _getNodesOfGroups;


	public static IServiceProvider Services {get; private set;}
	public override void _EnterTree()
	{
		base._EnterTree();

		

		BuildServices();
	}

	public override void _Ready()
    {
        base._Ready();


        // Ottieni il nodo TargetController dalla scena
        var targetController = this.GetNodesOfGroups(_getNodesOfGroups)
			.FirstOrDefault(n => n is TargetController) as TargetController;
        // Risolvi la factory dal container DI
        var factory = Services.GetRequiredService<ITargetResolverFactory>();
        // Iniettala nel TargetController
        targetController.SetTargetFactory(factory);
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
		
		services.AddTransient<SelectionTargetResolver>();
		services.AddTransient<ShapeTargetResolver>();

		services.AddSingleton<ITargetResolverFactory, TargetResolverFactory>();


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