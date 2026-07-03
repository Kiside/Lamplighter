using Godot;

namespace SystemLamplighter.DataStructure.GeneralData
{
	/// <summary>
	/// Struttura dati per ricavare la posizione dello shape target
	/// </summary>
	public class PositionCursorState : TargetCursorState
	{
		public Vector3 Position { get; private set; }

		public PositionCursorState(Vector3 position) : base(TargetResolutionStatus.ON_GOING)
		{
			Position = position;
		}
	}
}