using Godot;
using SystemLamplighter;
using Microsoft.Extensions.DependencyInjection;
using MessagePipe;
using System;
using SystemLamplighter.Interfaces;
using SystemLamplighter.Combat.Core;
using SystemLamplighter.Combat.Actor;

namespace SystemLamplighter.Bootstrap;

public partial class GameBootstrap : Node
{
	public static IServiceProvider Services {get; private set;}
	public override void _EnterTree()
	{
		base._EnterTree();

		BuildServices();
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