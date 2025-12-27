using SystemLamplighter;
using Characters;
public interface ICombatActor
{
	AtbCharacterProperties AtbProperties { get; }
    CombatLoadout CombatLoadout { get; }
    //AtbCharacterType CharacterType { get; }
}