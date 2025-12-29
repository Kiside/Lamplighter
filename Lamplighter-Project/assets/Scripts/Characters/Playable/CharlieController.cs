using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using SystemLamplighter;
using SystemLamplighter.BattleMenu;
using Characters.Interfaces;
using Characters.Inteaces;
using Microsoft.Extensions.DependencyInjection;
using MessagePipe;

namespace Characters.Playable
{
	public partial class CharlieController : CharacterController<CharlieView,CharlieModel>, 
	ICombatActor, ICombatCommandHandler ,ICombatActionExecutor
	{
		#region PUBLIC
		public AtbCharacterProperties AtbProperties => _model.AtbCharacterProperties;
		public CombatLoadout CombatLoadout { get => _model.CombatLoadout; set => _model.CombatLoadout = value; }
		#endregion

		#region PROTECTED/PRIVATE PROPERTIES 
		protected AbstractCombat<CharlieController> _combat { get => _model.Combat; set => _model.Combat = value; }
		protected AbstractMovement<CharlieController> _movement { get => _model.Movement; set => _model.Movement = value; }
		protected BattleMenuController _battleMenuController { get => _model.BattleMenu;}
		private bool _lockOn = false;
		private readonly DisposableBagBuilder _bag = DisposableBag.CreateBuilder();
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

			this.SubscribeEvent<AtbCommandPhaseStartedEvent>(OnCommandPhaseStarted).AddTo(_bag);
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

		public void OpenBattleSubMenuHandler(SubMenuType subMenuType)
		{
			List <IActionData> subMenuIds = new List<IActionData>();
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
		
		public override List<IActionData> GetAttacksId() => CombatLoadout.GetAttacksId();
		public override List<IActionData> GetMagicsId() => CombatLoadout.GetMagicsId();
		public override List<IActionData> GetItemsId() => CombatLoadout.GetItemsId();
		#endregion

		#region ICombatActionExecutor
		public void OnExecuteCombatAction(AtbCommandPhaseStartedEvent ev)
		{
			
		}
		#endregion

		#region ICombatCommandHandler
		public void OnCommandPhaseStarted(AtbCommandPhaseStartedEvent ev)
		{
			if(ev.Actor != this)
				return;
			
			// Richiamo Combat menu
			_battleMenuController.Show();
		}
		#endregion

		public override void _ExitTree()
		{
			_bag.Build().Dispose();

			base._ExitTree();
		}
	}
}
