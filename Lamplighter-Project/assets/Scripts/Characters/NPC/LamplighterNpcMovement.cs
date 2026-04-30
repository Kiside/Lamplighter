using Characters.Abstract;

namespace Characters.NPC;

/// <summary>
/// Classe per il movimento dei NPC
/// </summary>
public partial class LamplighterNpcMovement : AbstractMovement<NpCharacterController>
{
	public override void Init(NpCharacterController controller)
	{
		base.Init(controller);
	}

	public override Godot.Vector3 RealtimeMove(double delta)
	{
		return Godot.Vector3.Zero;
	}

	public override bool IsMovementFinished()
		{
			return true;
		}
}
