using System.Collections.Generic;

/// <summary>
/// Interfaccia per la classi che si occupano della logica di business per la risoluzione di "effetti"
/// </summary>
public interface IEffectResolver
{
	public CharacterProperties Statistics {get;}
	public float Resolve(List<IEffectData> effects);
	public void Init(CharacterProperties statistics);

}