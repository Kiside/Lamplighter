using SystemLamplighter;
using Characters;

namespace Characters.Interfaces
{
    public interface ICombatActor
    {
        public AtbCharacterProperties AtbProperties { get; }
        public AtbCharacterStatus AtbStatus {get;}
        public CombatLoadout CombatLoadout { get; }
        public IActionData CurrentAction  {get; set;}
        public AtbCharacterStatus UpdateAtbPosition(float value);
    }    
}
