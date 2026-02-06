using Godot;
using SystemLamplighter;

[GlobalClass]
public partial class TargetData : Resource, ITargetData
{
	[Export]
	private TargetType _targeType;
	[Export]
	private float _diameter;
	[Export]
	private int _numberOfTargets;
	[Export]
	private float _range;
	
	public TargetType TargetType {get{return _targeType;}}
	public float Diameter {get{return _diameter;}}
	public int NumberOfTargets {get{return _numberOfTargets;}}
	public float Range{get;}

	public TargetData () : this(TargetType.SINGLE, 0f, 1, 0f) {}

	public TargetData (TargetType targetType,
	float diameter,
	int numberOfTargets,
	float range)
	{
		_targeType = targetType;
		_diameter = diameter;
		_numberOfTargets = numberOfTargets;
		_range = range;
	}
}