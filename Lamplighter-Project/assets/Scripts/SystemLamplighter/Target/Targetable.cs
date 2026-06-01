using System;
using Godot;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.Debug;
using SystemLamplighter.Extensions;

public partial class Targetable : Node, ITargetable
{
	/// <summary>
	/// La posizione nello spazio dell'entità targetizzabile
	/// </summary>
	[Export]
	private Node3D _position; 
	/// <summary>
	/// La tipologia di entità targetizzabile
	/// </summary>
	[Export]
	private TargeTableType _targetableType = TargeTableType.Ally;
	/// <summary>
	/// Il nome da mostrare per il target
	/// </summary>
	[Export]
	private string _name = "";
	
	public Vector3 Position => _position.GlobalPosition;
	public TargeTableType TargeTableType => _targetableType;
	public string TargetableName => _name;
	public Identification Id {get; private set;}

	public override void _Ready()
	{
		base._Ready();
		if(_name == string.Empty)
			_name = GetParent().Name;

		if(Id == null)
		{
			Id = this.SetIdentification();
			// TODO Da cancellare 
			// if(GetParent() is IIdentificable parent)
			// 	Id = parent.Id;
			// else 
			// 	DebugLamplighter.Assert(true, "The parent is not IIdentificable");
		}
	}

	public void Select()
	{
		
	}

	public void Deselect()
	{
		

	}
}