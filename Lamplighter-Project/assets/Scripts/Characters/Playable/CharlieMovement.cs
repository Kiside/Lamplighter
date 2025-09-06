using Godot;
using System;
using System.Numerics;

public partial class CharlieMovement : AbstractMovement
{
	public override Godot.Vector3 Move(double delta)
	{
		GD.Print("Ciao");
		return Godot.Vector3.Zero;
	}

	
}