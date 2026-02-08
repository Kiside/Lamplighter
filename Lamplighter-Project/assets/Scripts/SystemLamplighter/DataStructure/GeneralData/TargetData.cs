using Godot;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.Interfaces;

namespace SystemLamplighter.DataStructure.GeneralData;

/// <summary>
/// Classe per i data dei target
/// </summary>
[GlobalClass]
public partial class TargetData : Resource, ITargetData
{
	/// <summary>
	/// Il tipo di target
	/// </summary>
	[Export]
	private TargetType _targeType;
	/// <summary>
	/// Il diametro del target, se il target non è una shape deve essere 0
	/// </summary>
	[Export]
	private float _diameter;
	/// <summary>
	/// Il numero di target, se non c'è un numero esatto deve essere 0
	/// </summary>
	[Export]
	private int _numberOfTargets;
	/// <summary>
	/// La gittata (range) del target, se non c'è una gittata deve essere 0
	/// </summary>
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