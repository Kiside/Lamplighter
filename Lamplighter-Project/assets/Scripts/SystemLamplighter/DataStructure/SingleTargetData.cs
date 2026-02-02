using Godot;
using System;
using SystemLamplighter;

[Serializable]
public partial class SingleTargetData : ITargetData
{
	public TargetType TargetType {get;}
	public float Size {get;}
	public int NumberOfTargets {get;}

	public SingleTargetData (TargetType targetType = TargetType.SINGLE,
	float size = 0f,
	int numberOfTargets = 1)
	{
		TargetType = targetType;
		Size = size;
		NumberOfTargets = 1;
	}
}