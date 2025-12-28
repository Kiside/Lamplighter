using SystemLamplighter;
using Characters;

namespace Characters.Interfaces
{
    public interface ICombatActor
    {
        AtbCharacterProperties AtbProperties { get; }
        CombatLoadout CombatLoadout { get; }
        //AtbCharacterType CharacterType { get; }
    }    
}
