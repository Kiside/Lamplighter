
using System.Collections.Generic;
using SystemLamplighter.Tool;
using SystemLamplighter.DataStructure.GeneralData;
using SystemLamplighter.Interfaces;

public class EffectResolver : IEffectResolver
{
	public CharacterProperties Statistics {get; private set;}

	public void Init(CharacterProperties statistics)
	{
		Statistics = statistics;
	}

	public float Resolve(List<IEffectData> effects)
	{
		Log.PrintMessage($"HEALTH: {Statistics.CurrentHealth}");
		foreach(var e in effects)
		{
			e.Apply(Statistics);
		}
		Log.PrintMessage($"HEALTH AFTER DAMAGE: {Statistics.CurrentHealth}");
		return 0f;
	}
}