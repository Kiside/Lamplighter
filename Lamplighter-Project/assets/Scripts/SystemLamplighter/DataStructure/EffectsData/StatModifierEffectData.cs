using Godot;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.DataStructure.GeneralData;

namespace SystemLamplighter.DataStructure.EffectsData;

/// <summary>
/// Classe che rappresenta i dati di modifiche alle stat
/// </summary>
[GlobalClass]
public partial class StatModifierEffectData : EffectData
{
	// Che tipologie di stat modifier è
	[Export]
	private ModifierKind _kind;
	// Quanto influisce sulla stat
	[Export]
	private float _amount;
	// Quale stat viene influenzata
	[Export]
	private Stat _statToModify;
	// Quanto dura l'effetto in tick
	[Export]
	private float _tickDuration;

	public Stat StatToModify => _statToModify;
	public float Amount => _amount;
	public ModifierKind Kind => _kind;
	public float TickDuration => _tickDuration;

	public StatModifierEffectData() : this(ModifierKind.BUFF, 0f, Stat.AGILITY, 0f) { }

	public StatModifierEffectData(ModifierKind kind, float amount, Stat statToModify, float tickDuration)
	{
		_kind = kind;
		_amount = amount;
		_statToModify = statToModify;
		_tickDuration = tickDuration;
	}



	public override void Apply(CharacterProperties target)
	{
		throw new System.NotImplementedException();
	}
}