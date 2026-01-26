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
	public partial class PlayableCharacterController : CharacterController<PlayableCharacterView,PlayableCharacterModel>, 
	ICombatActor, ICombatCommandHandler ,ICombatActionExecutor
	{
		#region PUBLIC
		public AtbCharacterProperties AtbProperties => _model.AtbCharacterProperties;
		public AtbCharacterStatus AtbStatus => _model.AtbCharacterProperties.Status;
		public CombatLoadout CombatLoadout { get => _model.CombatLoadout; set => _model.CombatLoadout = value; }
		public IActionData CurrentAction {get => _model.CurrentAction; set => _model.CurrentAction = value; }
		public ICombatActor CombatActor {get => _model.CombatActor;}
		public BattleMenuController BattleMenuController => _battleMenuController; 
		#endregion

		#region PROTECTED/PRIVATE PROPERTIES 
		protected AbstractCombat<PlayableCharacterController> _combat { get => _model.Combat; set => _model.Combat = value; }
		protected AbstractMovement<PlayableCharacterController> _movement { get => _model.Movement; set => _model.Movement = value; }
		protected BattleMenuController _battleMenuController { get => _model.BattleMenu;}
		protected List<string> _groups {get => _model.Groups;}
		private bool _lockOn = false;
		private readonly DisposableBagBuilder _bag = DisposableBag.CreateBuilder();
		protected IGroupsInitiator _groupsInitiator;
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
			_groupsInitiator = new GroupsInitiator(_groups, this);

			_combat.Init(this);
			_movement.Init(this);

			Subscribe();
		}

		#region Subscribe/Unsubscribe
		protected override void Subscribe()
		{
			// QUESTI DUE METODI SONO IMPORTANTI PER I PLAYERS
			_model.OnOpenBattleSubMenu += OpenBattleSubMenuHandler;
			_model.OnActionClicked += ActionChoosedHandler;

			// QUESTE DUE SOTTOSCRIZIONI POTREBBERO ESSERE IMPORTANTI PER I CHARACTERS CHE ENTRANO IN COMBATTIMENTO
			this.SubscribeEvent<AtbExecuteActionEvent>(OnExecuteCombatAction).AddTo(_bag);
			this.SubscribeEvent<AtbCommandPhaseStartedEvent>(OnCommandPhaseStarted).AddTo(_bag);
		}
		protected override void Unsubscribe()
		{
			_model.OnOpenBattleSubMenu -= OpenBattleSubMenuHandler;
			_model.OnActionClicked -= ActionChoosedHandler;

			_bag.Build().Dispose();
		}
		#endregion

		// TODO IL NODE CHECKING È DA CONTROLLARE BENE SE PUÒ ESSERE GENERALIZZATO ANCORA
		protected override void NodeChecking()
		{
			base.NodeChecking();

			string noNode = "There is no ";
			Debug.Assert(MovementNode != null, $"{noNode} MovementNode is null.");
			Debug.Assert(CombatNode != null, $"{noNode} CombatNode is null.");

			if (MovementNode != null)
				_movement = GetNode<AbstractMovement<PlayableCharacterController>>(MovementNode);

			if (CombatNode != null)
				_combat = GetNode<AbstractCombat<PlayableCharacterController>>(CombatNode);
		}

		public override void _PhysicsProcess(double delta)
		{
			_combat.Combat();
			Velocity = _movement.Move(delta);
			MoveAndSlide();
		}

		// QUESTA REGION È IMPORTANTE PER I PLAYERS
		// TODO: GLI EVENTI DEL BATTLE MENU ANCHE PROBABILMENTE NON DEVONO ESSRE QUI
		// TODO: NODO COMBAT?
		#region BATTLE MENU EVENTS
		public void OpenBattleSubMenuHandler(ISubMenuDefinition subMenu)
		{
			if(subMenu.IsImmediate)
			{
				CurrentAction = subMenu.BuildAction(this).First();
				this.PublishEvent<AtbCommandPhaseEndEvent>(new AtbCommandPhaseEndEvent(this));
			 	return;
			}
			
			var actions = subMenu.BuildAction(this);
			_battleMenuController.OpenSubMenu(actions);
		}

		public void ActionChoosedHandler()
		{
			// Che tipo di azione è? In base alla tipologia di azione ci saranno "cose da fare"
			switch(CurrentAction.ActionType)
			{
				case ActionType.ATTACK:
				HandleAttackAction();
				break;
				case ActionType.GUARD:
				HandleGuardAction();
				break;
				case ActionType.MAGIC:
				HandleMagicAction();
				break;
				case ActionType.ITEM:
				HandleItemAction();
				break;
				case ActionType.ESCAPE:
				HandleEscapeAction();
				break;
			}
			
			AtbProperties.EndCommandStatus(CurrentAction.ActionSpeedMultiplier);

			this.PublishEvent<AtbCommandPhaseEndEvent>(new AtbCommandPhaseEndEvent(this));
		}
		#endregion

		private void HandleAttackAction()
		{
			if(CurrentAction is AttackAction action)
			{
				switch(action.AttackType)
				{
					case AttackType.AREA:
					break;
					case AttackType.PUSH:
					break;
					// Il caso di default è per tutte le tipologie di attacco che hanno come selezione un singolo target
					default:
					SingleTargetAttack();
					break;
				}
			}
		}
		private void HandleGuardAction()
		{}
		private void HandleMagicAction()
		{}
		private void HandleItemAction()
		{}
		private void HandleEscapeAction()
		{}

		// TODO: QUESTO E MAGARI ALTRI METODI PER IL TARGETTING DEVONO ESSERE DEMANDATI
		private void SingleTargetAttack()
		{
			var combatActorRegistry = GameBootstrap.Services.GetRequiredService<ICombatActorRegistry>();
			var enemies = combatActorRegistry.GetActors(AtbCharacterType.ENEMY);

			// if(enemies != null)
			// {
			// 	foreach(var e in enemies)
			// 	{
			// 		Log.PrintMessage($"enemy: {e.AtbProperties.Name}");
			// 	}
			// }
			
		}

		// QUESTA REGION È IMPORTANTE PER I PLAYERS E FORSE ANCHE PER I CHARACTERS IN COMBATTIMENTO
		// TODO: CONTROLLARE SE POSSONO ESSERE MESSI ALTROVE QUESTI METODI, SEMPRE CLASSE/INTERFACCE
		#region COMBATLOADOUT METHODS
		public override List<IActionData> GetAttacksId() => CombatLoadout.GetAttacksId();
		public override List<IActionData> GetMagicsId() => CombatLoadout.GetMagicsId();
		public override List<IActionData> GetItemsId() => CombatLoadout.GetItemsId();
		public override List<IActionData> GetDefenseId() => CombatLoadout.GetDefenseId();
		#endregion

		// TODO: ANCHE QUESTI METODI POSSONO ESSERE DEMANDATI?
		// QUESTA REGION È IMPORTANTE PER I PLAYERS
		#region ICombatActionExecutor
		// QUESTO METODO POTREBBE ESSERE IMPORTANTE PER I CHARACTERS IN COMBATTIMENTO
		public void OnExecuteCombatAction(AtbExecuteActionEvent ev)
		{
			
			if(ev.Actor != this)
				return;
			
			// Eseguo l'azione
			// Ad azione eseguita resetto la posizione del personaggio sull'ATB
			this.PublishEvent(new AtbEndExecuteActionEvent(this));
		}
		#endregion

		#region ICombatCommandHandler
		// QUESTO METODO POTREBBE ESSERE IMPORTANTE PER I CHARACTERS IN COMBATTIMENTO
		public void OnCommandPhaseStarted(AtbCommandPhaseStartedEvent ev)
		{
			if(ev.Actor != this)
				return;
			
			// Richiamo Combat menu
			_battleMenuController.Show();
		}
		#endregion

		#region ICombatActor
		public AtbCharacterStatus UpdateAtbPosition(float value) => AtbProperties.UpdatePosition(value);
		#endregion

		public override void _ExitTree()
		{
			Unsubscribe();
			AtbProperties.Unsubscribe();
						
			base._ExitTree();
		}
	}
}
