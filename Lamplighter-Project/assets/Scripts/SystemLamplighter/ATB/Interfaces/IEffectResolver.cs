using System.Collections.Generic;

public interface IEffectResolver
{
	public CharacterStatistics Statistics {get;}
	public float Resolve(List<IEffectData> effects);
	public void Init(CharacterStatistics statistics);

}