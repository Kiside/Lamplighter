using Godot;
using System;


public partial class CharlieMovement : AbstractMovement
{
	public override Vector3 Move(double delta)
	{
		Vector3 direction = Vector3.Zero;

		if (Input.IsActionPressed(InputMap.Up))
		{
			direction.X += 1;
		}
		if (Input.IsActionPressed(InputMap.Down))
		{
			direction.X -= 1;
		}
		if (Input.IsActionPressed(InputMap.Left))
		{
			direction.Z -= 1;
		}
		if (Input.IsActionPressed(InputMap.Right))
		{
			direction.Z += 1;
		}

		if (direction != Vector3.Zero)
		{
			direction.Normalized();
		}

		_targetVelocity.X = direction.X * Speed;
		_targetVelocity.Z = direction.Z * Speed;


		return _targetVelocity;
	}


}