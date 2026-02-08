using Godot;
using Characters.Interfaces;
using SystemLamplighter.Common.Enums;

namespace SystemLamplighter.ATB.Interfaces;


public interface IAtbCharacterProperties
{
	public string Name {get;}
	public AtbCharacterType CharacterType {get;}
	public Image Avatar {get;}
	public float Speed {get;}
	public float SpeedMultiplier {get; set;}
	public float Position {get;}
	public AtbCharacterStatus Status {get;}

	public void Init(Image avatar, float speed, string name, AtbCharacterType atbCharacterType,ICharacterControllerAtb character);
	public AtbCharacterStatus UpdatePosition(float value);

	public void Subscribe();
	public void Unsubscribe();
}
