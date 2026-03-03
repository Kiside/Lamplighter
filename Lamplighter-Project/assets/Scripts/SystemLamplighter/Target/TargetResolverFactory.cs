using System;
using LamplighterPlugins.CustomNodes;
using Microsoft.Extensions.DependencyInjection;
using SystemLamplighter.Bootstrap;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.Target;
using SystemLamplighter.Target.Interfaces;

public class TargetResolverFactory : ITargetResolverFactory
{
	private readonly IServiceProvider _serviceProvider;
	
	public TargetResolverFactory(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	public ITargetResolver Create(TargetType targetType)
	{
		return targetType switch
		{
			TargetType.SINGLE or TargetType.GROUP => GameBootstrap.Services.GetRequiredService<SelectionTargetResolver>(),
			TargetType.CIRCLE or TargetType.LINE or TargetType.CONE => GameBootstrap.Services.GetRequiredService<ShapeTargetResolver>(),
			_ => null
		};
	}
}