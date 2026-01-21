using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SystemLamplighter;
using SystemLamplighter.BattleMenu;
using SystemLamplighter.Debug;

namespace Characters.Playable
{
	public partial class PlayableCharacterModel : AbstractModel
	{
		#region EXPORT PROPERTIES
		[Export]
		protected NodePath _combatLoadoutNode;
		[Export]
		protected AtbCharacterProperties _atbCharacterProperties;
		[Export]
		protected NodePath _battleMenuNode;
		[Export]
		protected Godot.Collections.Array<GroupsName> _groups;
		#endregion

		#region PROTECTED PROPERTIES 
		protected AbstractCombat<PlayableCharacterController> _combat;
		protected AbstractMovement<PlayableCharacterController> _movement;

		protected BattleMenuController _battleMenu;

		protected CombatLoadout _combatLoadout;

		protected IActionData _currentAction;

		private bool _lockOn = false;
		#endregion

		#region PUBLIC PROPERTIES
		public AbstractCombat<PlayableCharacterController> Combat { get => _combat; set => _combat = value; }
		public AbstractMovement<PlayableCharacterController> Movement { get => _movement; set => _movement = value; }
		public BattleMenuController BattleMenu {get => _battleMenu;}
		public CombatLoadout CombatLoadout { get => _combatLoadout; set => _combatLoadout = value; }
		public NodePath CombatLoadoutNode {get => _combatLoadoutNode;}
		public AtbCharacterProperties AtbCharacterProperties => _atbCharacterProperties;
		public bool LockOn { get => _lockOn; set => _lockOn = value; }
		public IActionData CurrentAction {get => _currentAction; set => _currentAction = value; }
		public List<string> Groups {get => _groups.Select(g => g.ToString()).ToList<string>();}
		#endregion

		public event Action<SubMenuType> OnOpenBattleSubMenu;  
		public event Action OnActionClicked;

		public override void Init()
		{
			NodeChecking();
			SubscribeBattleMenu();
		}

		private void SubscribeBattleMenu() 
		{
			_battleMenu.OnOpenSubMenu += TriggerOnOpenSubMenu;
			_battleMenu.OnActionClick += TriggerOnActionClicked;
		}
		private void UnsubscribeBattleMenu() 
		{
			_battleMenu.OnOpenSubMenu -= TriggerOnOpenSubMenu;
			_battleMenu.OnActionClick -= TriggerOnActionClicked;
		}


		private void TriggerOnOpenSubMenu(SubMenuType subMenuType)
		{
			DebugLamplighter.Assert(subMenuType != SubMenuType.NONE, "subMenuType is NONE");

			OnOpenBattleSubMenu?.Invoke(subMenuType);
		}

		private void TriggerOnActionClicked(IActionData actionData)
		{
			DebugLamplighter.Assert(actionData != null, "actionData is null");

			_currentAction = actionData;
			OnActionClicked?.Invoke();
		}

		private void NodeChecking()
		{
			string noNode = "There is no ";
			DebugLamplighter.Assert(_combatLoadoutNode != null, $"{noNode} CombatLoadout is null");
			DebugLamplighter.Assert(_atbCharacterProperties != null, $"{noNode} AtbCharacterProperties is null");
			DebugLamplighter.Assert(_battleMenuNode != null, $"{noNode} battleMenuNode is null");

			if(_combatLoadoutNode != null)
				_combatLoadout  = GetNode<CombatLoadout>(_combatLoadoutNode);

			if(_battleMenuNode != null)
				_battleMenu = GetNode<BattleMenuController>(_battleMenuNode);
		}

		public override void _ExitTree()
		{
			UnsubscribeBattleMenu();
			base._ExitTree();
		}
	}
}