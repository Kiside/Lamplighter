using System;
using Microsoft.Extensions.DependencyInjection;
using SystemLamplighter.Combat.Core;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.Interfaces;

namespace SystemLamplighter.Combat.Core;


/// <summary>
/// Factory per la scelta di quale tipologia di TurnBasedCombat bisogna ritornare in base al giocatore
/// </summary>
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