
using System.Collections.Generic;
using SystemLamplighter.Tool;

public class EffectResolver : IEffectResolver
{
	public float CurrentHealth {get; private set;}

	public float MaxHealth {get; private set;}

	public float TempHealth {get; private set;}

	public void SetHealth(float health)
	{
		CurrentHealth = health;
		MaxHealth = health;
		TempHealth = 0f;
	}
	public float Resolve(List<IEffectData> effects)
	{
		Log.PrintMessage("RESOLVE EFFECT");
		return 0f;
	}
}