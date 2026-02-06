using Godot;

[GlobalClass]
public partial class HealData : EffectData
{
	[Export]
	private float _healAmount;

	public float HealAmount=> _healAmount;

	public override void Apply()
	{
		throw new System.NotImplementedException();
	}
}