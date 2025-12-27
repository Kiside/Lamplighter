using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using SystemLamplighter;

namespace Characters.Playable
{
	public partial class CharlieModel : AbstractModel
	{
		#region EXPORT PROPERTIES
		[Export]
		protected NodePath _combatLoadoutNode;
		[Export]
		protected AtbCharacterProperties _atbCharacterProperties;
		#endregion

		#region PROTECTED PROPERTIES 
		protected AbstractCombat<CharlieController> _combat;
		protected AbstractMovement<CharlieController> _movement;

		protected CombatLoadout _combatLoadout;

		private bool _lockOn = false;
		#endregion

		#region PUBLIC PROPERTIES
		public AbstractCombat<CharlieController> Combat { get => _combat; set => _combat = value; }
		public AbstractMovement<CharlieController> Movement { get => _movement; set => _movement = value; }
		public CombatLoadout CombatLoadout { get => _combatLoadout; set => _combatLoadout = value; }
		public AtbCharacterProperties AtbCharacterProperties;
		public bool LockOn { get => _lockOn; set => _lockOn = value; }
		#endregion
 

		public override void Init()
		{
			NodeChecking();
		}

		private void NodeChecking()
		{
			string noNode = "There is no ";
			Debug.Assert(_combatLoadoutNode != null, $"{noNode} CombatLoadout is null");


			if(_combatLoadoutNode != null)
				_combatLoadout  = GetNode<CombatLoadout>(_combatLoadoutNode);
		}
	}
}