using Godot;
using System;
using SystemLamplighter;

namespace SystemLamplighter.BattleMenu
{
	public partial class BattleMenuView : AbstractView
	{
		[Export]
		private NodePath _attackButtonPath;
		[Export]
		private NodePath _magicButtonPath;
		[Export]
		private NodePath _guardButtonPath;
		[Export]
		private NodePath _itemButtonPath;

		[Export]
		private Control _SubMenuContainer;

		Button _attackButton;
		Button _magicButton;
		Button _guardButton;
		Button _itemButton;

		public override void Init()
		{
			NodeChecking();
		}

		private void NodeChecking()
		{
			if (_attackButtonPath is null)
				Log.PrintWarning("AttackButton Path missing");
			if (_magicButtonPath is null)
				Log.PrintWarning("MagicButton Path missing");
			if (_guardButtonPath is null)
				Log.PrintWarning("GuardButton Path missing");
			if (_itemButtonPath is null)
				Log.PrintWarning("ItemButton Path missing");
		}

		public void OnMainButtonBattleMenuClicked()
		{

		}
	}
}