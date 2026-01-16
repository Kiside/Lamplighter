namespace Characters.NPC
{
	public partial class LamplighterNpcMovement : AbstractMovement<NpCharacterController>
	{
		public override void Init(NpCharacterController controller)
		{
			base.Init(controller);
		}
		
		public override Godot.Vector3 Move(double delta)
		{
			return Godot.Vector3.Zero;
		}
	}
}