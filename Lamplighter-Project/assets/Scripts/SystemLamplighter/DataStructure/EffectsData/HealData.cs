using Godot;

namespace SystemLamplighter.DataStructure.EffectsData;

/// <summary>
/// Classe che rappresenta i dati di cura
/// </summary>
[GlobalClass]
public partial class HealData : EffectData
{
	[Export]
	private float _healAmount;

	public float HealAmount=> _healAmount;

	public override void Apply(CharacterStatistics target)
	{
		throw new System.NotImplementedException();
	}
}