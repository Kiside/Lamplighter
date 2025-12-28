using Godot;
using SystemLamplighter;
using Microsoft.Extensions.DependencyInjection;
using MessagePipe;
using System;

public partial class GameManager : Node
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

		services.AddMessagePipe();

		// Se vuoi registrare altri servizi globali, fallo qui
        // services.AddSingleton<IMyService, MyService>();

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