using Godot;

namespace SystemLamplighter.DataStructure.EffectsData;

[GlobalClass]
/// <summary>
/// Metodo astratto per gli effetti delle Action
// </summary>
public abstract partial class EffectData : Resource
{
	public abstract void Apply();
} 