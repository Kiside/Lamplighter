using SystemLamplighter;
using Characters;
using Characters.Loadout;
using SystemLamplighter.ATB;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.Interfaces;
using System.Numerics;
using System;

namespace Characters.Interfaces;

/// <summary>
/// Interfaccia per i personaggi che possono partecipare al combattimento
/// </summary>
public interface ICombatActor
{
    public IAtbCharacterService CharacterService {get; }
    public AtbCharacterStatus AtbStatus { get; }
    public CombatLoadout CombatLoadout { get; }
    public IActionData CurrentAction { get; set; }
    public Identification Id {get;}
    public AtbCharacterProperties AtbProperties {get;}
    public AtbCharacterStatus UpdateAtbPosition(float value);
    public void Init(IAtbCharacterService atbCharacterService);
    public void Unsubscribe();
    public void Subscribe();
}
