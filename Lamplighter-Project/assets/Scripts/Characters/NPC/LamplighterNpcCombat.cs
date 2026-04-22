using System.Collections.Generic;
using Characters.Abstract;

namespace Characters.NPC;

/// <summary>
/// Classe per il combattimento dei NPC
/// </summary>
public partial class LamplighterNpcCombat : AbstractCombat<NpCharacterController>
{
	public override void Init(NpCharacterController controller)
	{
		base.Init(controller);
	}

	public override void Combat()
	{

	}

	public override  Queue<Godot.Vector3> Move()
	{
		return new Queue<Godot.Vector3> ();
	}
}
