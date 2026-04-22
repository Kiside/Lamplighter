using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Characters.Abstract
{
	/// <summary>
	/// Classe astratta per il combattimento
	/// </summary>
	/// <typeparam name="TController">Controller</typeparam>
	public abstract partial class AbstractCombat<TController> : Node
	where TController : AbstractCharacterController
	{
		[Export]
		public float Damage = 0;

		TController _controller;

		public virtual void Init(TController controller)
		{
			Debug.Assert(controller != null, "Controller is null");
			_controller = controller;
		}

		public abstract void Combat();

		public abstract Queue<Godot.Vector3> Move();
	}
}

