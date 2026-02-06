using Godot;
using SystemLamplighter.DataStructure;
using System;

[GlobalClass]
[Serializable]
public partial class NewActionData : EquipableActionData
{
	//DamageData damageData;
	[Export]
	int prova;

	public NewActionData() : this(0) {}

	public NewActionData(int p)
	{
		prova = p;
	}
}