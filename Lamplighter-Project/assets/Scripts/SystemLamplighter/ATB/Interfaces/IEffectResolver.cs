using System.Collections.Generic;

public interface IEffectResolver
{
	public CharacterProperties Statistics {get;}
	public float Resolve(List<IEffectData> effects);
	public void Init(CharacterProperties statistics);

}