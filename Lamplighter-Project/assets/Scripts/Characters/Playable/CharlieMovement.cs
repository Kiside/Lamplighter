using Godot;
using System;
using SystemLamplighter;

namespace Characters.Playable
{
	public partial class CharlieMovement : AbstractMovement<CharlieController>
	{
		public override void Init(CharlieController controller)
		{
			base.Init(controller);
		}

		public override Vector3 Move(double delta)
		{
			Vector3 direction = Vector3.Zero;

			if (Input.IsActionPressed(SystemLamplighter.InputMap.Up))
			{
				direction.X += 1;
			}
			if (Input.IsActionPressed(SystemLamplighter.InputMap.Down))
			{
				direction.X -= 1;
			}
			if (Input.IsActionPressed(SystemLamplighter.InputMap.Left))
			{
				direction.Z -= 1;
			}
			if (Input.IsActionPressed(SystemLamplighter.InputMap.Right))
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

		public void FreeMove()
		{

		}

		public void CombatMove()
		{

		}
	}
}
