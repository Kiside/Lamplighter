using System;
using Godot;
using SystemLamplighter.Common.Enums;

public interface ITargetable
{
	public Identification Id {get;}
	public Vector3 Position {get;}
	TargeTableType TargeTableType {get;}
	public string TargetableName {get;}

	public void Select();

	public void Deselect();
}