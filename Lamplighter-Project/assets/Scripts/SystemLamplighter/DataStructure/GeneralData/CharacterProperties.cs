
using System.Security.Cryptography.X509Certificates;
using Godot;
using SystemLamplighter.ATB;

[GlobalClass]
public partial class CharacterProperties : Resource
{
	[Export]
	public float Strenght;
	[Export]
	public float Dexterity;
	[Export]
	public float Constitution;
	[Export]
	public float Wisdom;
	[Export]
	public float Defense;
	[Export]
	public float Armor;
	[Export]
	public float MaxHealth;
	[Export]
	public float CurrentHealth;
	[Export]
	public float TempHealth;
	[Export]
	public AtbCharacterProperties AtbCharacterProperties;

	public float AtbSpeed => AtbCharacterProperties.Speed;
	public float AtbSpeedMultiplier => AtbCharacterProperties.SpeedMultiplier;
	

	public CharacterProperties() : this(0f,0f,0f,0f,0f,0f,0f,0f,0f, new AtbCharacterProperties()) {}

	public CharacterProperties(float str, float dex, float cons, float wis, float def, float arm, float maxHealth, float currentHealth, float tempHealth, AtbCharacterProperties atbCharacterProperties)
	{
		Strenght = str;
		Dexterity = dex;
		Constitution = cons;
		Wisdom = wis;
		Defense = def;
		Armor = arm;
		MaxHealth = maxHealth;
		CurrentHealth = currentHealth;
		TempHealth = tempHealth;
		AtbCharacterProperties = atbCharacterProperties;
	}

	
}