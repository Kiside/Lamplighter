using Godot;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.DataStructure.GeneralData;

namespace SystemLamplighter.DataStructure.EffectsData;
/// <summary>
/// Classe che rappresenta i dati dei danni nel tempo
/// </summary>
[GlobalClass]
public partial class DotDamageEffectData : EffectData
{
	// danno nel tempo
	[Export]
	private float _dotDamage;
	// Quanti tick di danno
	[Export]
	private int _dotTick;
	// Tipo di danno
	[Export]
	private DotDamageType _dotDamageType;

	public float DotDamage => _dotDamage;
	public int DotTick => _dotTick;
	public DotDamageType DotDamageType => _dotDamageType;

	public DotDamageEffectData() : this(0f, 0, DotDamageType.BURN) { }

	public DotDamageEffectData(float dotDamage, int dotTick, DotDamageType dotDamageType)
	{
		_dotDamage = dotDamage;
		_dotTick = dotTick;
		_dotDamageType = dotDamageType;
	}

	public override void Apply(CharacterProperties target)
	{
		throw new System.NotImplementedException();
	}
}