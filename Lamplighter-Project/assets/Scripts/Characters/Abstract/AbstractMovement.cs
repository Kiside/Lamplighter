using Godot;
using System;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using SystemLamplighter.DataStructure.GeneralData;


namespace Characters.Abstract
{
	/// <summary>
	/// Classe astratta per il movimento
	/// </summary>
	/// <typeparam name="TController"></typeparam>
	public abstract partial class AbstractMovement<TController> : Node
	where TController : AbstractCharacterController
	{
		[Export]
		public int Speed = 14;
		[Export]
		public int Fall_acceleration = 75;
		[Export]
		private bool _disable = false;

		
		public bool Disable {get => _disable; set => _disable = value;}

		protected TController _controller;

		protected Vector3 _targetVelocity = Vector3.Zero;

		public virtual void Init(TController controller)
		{
			Debug.Assert(controller != null, "Controller is null");
			_controller = controller;
		}

		public virtual Godot.Vector3 RealtimeMove(double delta)
		{
			return Godot.Vector3.Zero;
		}

		public virtual Godot.Vector3? PointToPointMove(Godot.Vector3 from, Godot.Vector3 to, Identification id) 
		{
			return new Godot.Vector3();
		}

		public abstract bool IsMovementFinished(); 
	}
}

