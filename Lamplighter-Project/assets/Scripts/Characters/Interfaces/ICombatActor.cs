using SystemLamplighter;
using Characters;

namespace Characters.Interfaces
{
    public interface ICombatActor
    {
        AtbCharacterProperties AtbProperties { get; }
        AtbCharacterStatus AtbStatus {get;}
        CombatLoadout CombatLoadout { get; }
        IActionData CurrentAction  {get; }
        public AtbCharacterStatus UpdateAtbPosition(float value);
    }    
}
