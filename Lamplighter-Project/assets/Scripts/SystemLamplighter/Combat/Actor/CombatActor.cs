using Characters;
using Characters.Interfaces;
using SystemLamplighter;
using Characters.Loadout;
using SystemLamplighter.ATB;
using SystemLamplighter.Interfaces;
using SystemLamplighter.Common.Enums;
using System.Numerics;
using System;
using Godot;
using SystemLamplighter.Extensions;
using Characters.Playable;
using SystemLamplighter.ATB.Interfaces;

namespace SystemLamplighter.Combat.Actor;

/// <summary>
/// Classe per i personaggi che possono combattere
/// </summary>
public partial class CombatActor : Node, ICombatActor
{
	#region Export variable
	[Export]
	public CombatLoadout CombatLoadout {get; private set;}
	#endregion

	#region Private variable
	private IActionData _currentAction;
	#endregion 

	#region Public variables
	public IAtbCharacterService CharacterService {get ; private set;}
	public AtbCharacterStatus AtbStatus 
	{get {return CharacterService.CharacterProperties.AtbCharacterStatus;}}
	public AtbCharacterProperties AtbProperties 
	{get {return CharacterService.CharacterProperties.AtbCharacterProperties;}}
	public IActionData CurrentAction 
	{get => _currentAction; set {_currentAction = value;}}
	public Identification Id {get; private set;}
	#endregion

	#region Methods

	public override void _Ready()
	{
		if(Id == null)
			Id = this.SetIdentification();

		base._Ready();
	}

	public void Init(IAtbCharacterService characterService)
	{
		CharacterService = characterService;
	}

	public AtbCharacterStatus UpdateAtbPosition(float value) 
		=> CharacterService.UpdatePosition(value);

	public void Unsubscribe() => CharacterService.Unsubscribe();
	public void Subscribe() => CharacterService.Subscribe();
	#endregion
}