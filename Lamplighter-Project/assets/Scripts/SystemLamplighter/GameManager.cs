using Godot;
using SystemLamplighter;
using Microsoft.Extensions.DependencyInjection;
using System;

public partial class GameManager : Node
{
	public static ServiceProvider Services;
	public override void _EnterTree()
	{
		base._EnterTree();

		SubscribeServices();
	}

	private void SubscribeServices()
	{
		var Services = new ServiceCollection();
	}


	public override void _ExitTree()
	{
		Log.Dispose();
	}
}