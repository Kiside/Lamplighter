using System;
using Microsoft.Extensions.DependencyInjection;
using SystemLamplighter.Combat.Core;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.Interfaces;

public class TurnBasedCombatFactory : ITurnBasedCombatFactory
{
	private readonly IServiceProvider _serviceProvider;

	public TurnBasedCombatFactory(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	public ITurnBasedCombat Create(AtbCharacterType characterType)
	{
		return characterType switch
		{
			AtbCharacterType.ALLY => _serviceProvider.GetRequiredService<TurnBasedCombat>(),
			AtbCharacterType.ENEMY => _serviceProvider.GetRequiredService<NpcTurnBasedCombat>(),
			_ => null
		};
	}
}