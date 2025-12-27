using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using SystemLamplighter;
using SystemLamplighter.BattleMenu;
using SystemLamplighter.Debug;

namespace Characters.Playable
{
	public partial class CharlieModel : AbstractModel
	{
		#region EXPORT PROPERTIES
		[Export]
		protected NodePath _combatLoadoutNode;
		[Export]
		protected AtbCharacterProperties _atbCharacterProperties;

		[Export]
		protected NodePath _battleMenuNode;
		#endregion

		#region PROTECTED PROPERTIES 
		protected AbstractCombat<CharlieController> _combat;
		protected AbstractMovement<CharlieController> _movement;

		protected BattleMenuController _battleMenu;

		protected CombatLoadout _combatLoadout;

		private bool _lockOn = false;
		#endregion

		#region PUBLIC PROPERTIES
		public AbstractCombat<CharlieController> Combat { get => _combat; set => _combat = value; }
		public AbstractMovement<CharlieController> Movement { get => _movement; set => _movement = value; }
		public BattleMenuController BattleMenu {get => _battleMenu;}
		public CombatLoadout CombatLoadout { get => _combatLoadout; set => _combatLoadout = value; }
		public NodePath CombatLoadoutNode {get => _combatLoadoutNode;}
		public AtbCharacterProperties AtbCharacterProperties => _atbCharacterProperties;
		public bool LockOn { get => _lockOn; set => _lockOn = value; }
		#endregion

		public event Action<SubMenuType> OnOpenBattleSubMenu;  

		public override void Init()
		{
			NodeChecking();
			SubscribeBattleMenu();
		}

		private void SubscribeBattleMenu() => _battleMenu.OnOpenSubMenu += TriggerOnOpenSubMenu;
		private void UnsubscribeBattleMenu() => _battleMenu.OnOpenSubMenu -= TriggerOnOpenSubMenu;

		private void TriggerOnOpenSubMenu(SubMenuType subMenuType)
		{
			OnOpenBattleSubMenu?.Invoke(subMenuType);
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