using Godot;

namespace SystemLamplighter.DataStructure.GeneralData
{
	public class PositionCursorState : TargetCursorState
	{
		public Vector3 Position { get; private set; }

		public PositionCursorState(Vector3 position) : base(TargetResolutionStatus.ON_GOING)
		{
			Position = position;
		}
	}
}