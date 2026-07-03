using System;
using Godot;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.DataStructure.GeneralData;

namespace SystemLamplighter.Target.Interfaces;

public interface ITargetable
{
	public Identification Id {get;}
	public Vector3 Position {get;}
	TargeTableType TargeTableType {get;}
	public string TargetableName {get;}

	public void Select();

	public void Deselect();
}