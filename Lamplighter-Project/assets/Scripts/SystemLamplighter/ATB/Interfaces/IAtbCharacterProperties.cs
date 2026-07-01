using Godot;
using Characters.Interfaces;
using SystemLamplighter.Common.Enums;

namespace SystemLamplighter.ATB.Interfaces;


/// <summary>
/// Interfaccia per le proprietà di un personaggio nell'ATB
/// </summary>
public interface IAtbCharacterProperties
{
	public string Name {get;}
	public AtbCharacterType CharacterType {get;}
	public Image Avatar {get;}
	public float Speed {get;}
	public float SpeedMultiplier {get; set;}
	public float Position {get;}
	public AtbCharacterStatus Status {get;}
}
