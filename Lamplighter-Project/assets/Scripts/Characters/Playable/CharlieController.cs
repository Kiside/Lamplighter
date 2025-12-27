using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using SystemLamplighter;
using SystemLamplighter.BattleMenu;

namespace Characters.Playable
{
	public partial class CharlieController : CharacterController<CharlieView,CharlieModel>, ICharacterControllerAtb
	{
		#region PROTECTED/PRIVATE PROPERTIES 
		protected AtbCharacterProperties _atbCharacterProperties => _model.AtbCharacterProperties;
		protected AbstractCombat<CharlieController> _combat { get => _model.Combat; set => _model.Combat = value; }
		protected AbstractMovement<CharlieController> _movement { get => _model.Movement; set => _model.Movement = value; }
		protected CombatLoadout _combatLoadout { get => _model.CombatLoadout; set => _model.CombatLoadout = value; }
		protected BattleMenuController _battleMenuController { get => _model.BattleMenu;}
		private bool _lockOn = false;
		#endregion
		
		#region PUBLIC PROPERTIES
		public bool LockOn { get { return _lockOn; } set { _lockOn = value; } }
		#endregion

		public override void _Ready()
		{
			base._Ready();
		}

		protected override void OnInit()
		{
			_combat.Init(this);
			_movement.Init(this);

			_model.OnOpenBattleSubMenu += OpenBattleSubMenuHandler;
		}

		protected override void NodeChecking()
		{
			base.NodeChecking();

			string noNode = "There is no ";
			Debug.Assert(MovementNode != null, $"{noNode} MovementNode is null.");
			Debug.Assert(CombatNode != null, $"{noNode} CombatNode is null.");

			if (MovementNode != null)
				_movement = GetNode<AbstractMovement<CharlieController>>(MovementNode);

			if (CombatNode != null)
				_combat = GetNode<AbstractCombat<CharlieController>>(CombatNode);
		}

		public override void _PhysicsProcess(double delta)
		{
			_combat.Combat();
			Velocity = _movement.Move(delta);
			MoveAndSlide();
		}

		#region ICharacterControllerAtb Methods
		public void ATB_Action()
		{

		}

		public AtbCharacterProperties ATB_GetCharacterProperties() => _atbCharacterProperties;
		#endregion

		public void OpenBattleSubMenuHandler(SubMenuType subMenuType)
		{
			List <string> subMenuIds = new List<string>();
			switch(subMenuType)
			{
				case SubMenuType.ATTACK:
					subMenuIds = GetAttacksId();
				break;
				case SubMenuType.MAGIC:
					subMenuIds = GetMagicsId();
				break;
				case SubMenuType.ITEMS:
					subMenuIds = GetItemsId();
				break;
			}
			
			_battleMenuController.OpenSubMenu(subMenuType, subMenuIds);
		}

		

		#region COMBATLOADOUT METHODS
		
		public override List<string> GetAttacksId() => _combatLoadout.GetAttacksId();
		public override List<string> GetMagicsId() => _combatLoadout.GetMagicsId();
		public override List<string> GetItemsId() => _combatLoadout.GetItemsId();

		#endregion
	}
}
