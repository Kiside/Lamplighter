using Godot;
using SystemLamplighter.DataStructure;
using System;

[Serializable]
public class DamageData
{
	ITargetData targetData;
	private float _damage;
	private float _pushbackForce;
	private float _atbPositionDamage;
}