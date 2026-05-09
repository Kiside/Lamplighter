using SystemLamplighter.Common.Enums;
using SystemLamplighter.Interfaces;

public interface ITurnBasedCombatFactory
{
	public ITurnBasedCombat Create(AtbCharacterType characterType);
}