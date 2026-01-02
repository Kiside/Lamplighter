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
using SystemLamplighter.Events;
using SystemLamplighter.Extensions;
using System.Linq;

namespace Characters.Playable
{
	public partial class CharlieController : CharacterController<CharlieView,CharlieModel>, 
	ICombatActor, ICombatCommandHandler ,ICombatActionExecutor
	{
		#region PUBLIC
		public AtbCharacterProperties AtbProperties => _model.AtbCharacterProperties;
		public CombatLoadout CombatLoadout { get => _model.CombatLoadout; set => _model.CombatLoadout = value; }
		public IActionData CurrentAction {get => _model.CurrentAction; set => _model.CurrentAction = value; }
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

			Subscribe();
		}

		private void Subscribe()
		{
			_model.OnOpenBattleSubMenu += OpenBattleSubMenuHandler;
			_model.OnActionClicked += ActionChoosedHandler;

			this.SubscribeEvent<AtbExecuteActionEvent>(OnExecuteCombatAction).AddTo(_bag);
			this.SubscribeEvent<AtbCommandPhaseStartedEvent>(OnCommandPhaseStarted).AddTo(_bag);
		}
		private void Unsubscribe()
		{
			_model.OnOpenBattleSubMenu -= OpenBattleSubMenuHandler;
			_model.OnActionClicked -= ActionChoosedHandler;

			_bag.Build().Dispose();
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


		#region BATTLE MENU EVENTS
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
				case SubMenuType.DEFEND:
					CurrentAction = GetDefenseId().First();
					this.PublishEvent<AtbCommandPhaseEndEvent>(new AtbCommandPhaseEndEvent(this));
					return;
			}

			_battleMenuController.OpenSubMenu(subMenuType, subMenuIds);
		}

		public void ActionChoosedHandler()
		{
			this.PublishEvent<AtbCommandPhaseEndEvent>(new AtbCommandPhaseEndEvent(this));
		}
		#endregion

		#region COMBATLOADOUT METHODS
		
		public override List<IActionData> GetAttacksId() => CombatLoadout.GetAttacksId();
		public override List<IActionData> GetMagicsId() => CombatLoadout.GetMagicsId();
		public override List<IActionData> GetItemsId() => CombatLoadout.GetItemsId();
		public override List<IActionData> GetDefenseId() => CombatLoadout.GetDefenseId();
		#endregion

		#region ICombatActionExecutor
		public void OnExecuteCombatAction(AtbExecuteActionEvent ev)
		{
			
			if(ev.Actor != this)
				return;
			
			Log.PrintMessage("ESEGUO L'AZIONE");
			// Eseguo l'azione
			// Ad azione eseguita resetto la posizione del personaggio sull'ATB
			this.PublishEvent(new AtbEndExecuteActionEvent(this));
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
			Unsubscribe();
			AtbProperties.Unsubscribe();
			base._ExitTree();
		}
	}
}
