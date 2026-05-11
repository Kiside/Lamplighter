using System.Collections.Generic;

public interface IEffectResolver
{
	public float CurrentHealth {get;}
	public float MaxHealth {get;}
	public float TempHealth {get;}
	public void SetHealth(float health);
	public float Resolve(List<IEffectData> effects);

}