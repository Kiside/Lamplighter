using Godot;

namespace SystemLamplighter.DataStructure.EffectsData;

[GlobalClass]
/// <summary>
/// Metodo astratto per gli effetti delle Action
// </summary>
public abstract partial class EffectData : Resource, IEffectData
{
	// Ogni apply sarà diverso, quindi quando si dovrà applicare l'effetto ogni classe
	// derivata da EffectData potrà dire cosa capita 
	public abstract void Apply();
} 