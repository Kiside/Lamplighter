using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
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
		private NodePath _subMenuContainerPath;


		private VBoxContainer _subMenuContainer;
		private ButtonUi _attackButton;
		private ButtonUi _magicButton;
		private ButtonUi _guardButton;
		private ButtonUi _itemButton;

		public string AttackButtonId => _attackButton.Name;
		public string MagicButtonId => _magicButton.Name;
		public string GuardButtonId => _guardButton.Name;
		public string ItemButtonId => _itemButton.Name;

		public event Action<SubMenuType> OnSubMenu;
		public event Action<string> OnActionClick;

		public override void Init()
		{
			NodeChecking();

			GetNodes();
			Subscribe();
		}

		private void GetNodes()
		{
			_attackButton = GetNode<ButtonUi>(_attackButtonPath);
			_magicButton = GetNode<ButtonUi>(_magicButtonPath);
			_guardButton = GetNode<ButtonUi>(_guardButtonPath);
			_itemButton = GetNode<ButtonUi>(_itemButtonPath);
			_subMenuContainer = GetNode<VBoxContainer>(_subMenuContainerPath);
		}

		private void Subscribe()
		{
			Debug.Assert(_attackButton != null);
			_attackButton.OnClick += HandleClick;
			Debug.Assert(_magicButton != null);
			_magicButton.OnClick += HandleClick;
			Debug.Assert(_guardButton != null);
			_guardButton.OnClick += HandleClick;
			Debug.Assert(_itemButton != null);
			_itemButton.OnClick += HandleClick;
		}

		private void Unsubscribe()
		{
			Debug.Assert(_attackButton != null);
			_attackButton.OnClick -= HandleClick;
			Debug.Assert(_magicButton != null);
			_magicButton.OnClick -= HandleClick;
			Debug.Assert(_guardButton != null);
			_guardButton.OnClick -= HandleClick;
			Debug.Assert(_itemButton != null);
			_itemButton.OnClick -= HandleClick;
		}

		private void HandleClick(string idButton)
		{
			switch (idButton)
			{
				case var _ when idButton == AttackButtonId:
					AttackHandle();
					break;
				case var _ when idButton == MagicButtonId:
					MagicButtonHandle();
					break;
				case var _ when idButton == GuardButtonId:
					GuardButtonHandle(idButton);
					break;
				case var _ when idButton == ItemButtonId:
					ItemButtonHandle();
					break;
				default:

					break;
			}
		}

		private void AttackHandle() => OnSubMenu?.Invoke(SubMenuType.ATTACK);
		private void MagicButtonHandle() => OnSubMenu?.Invoke(SubMenuType.MAGIC);
		private void GuardButtonHandle(string idButton) => OnActionClick?.Invoke(idButton);
		private void ItemButtonHandle() => OnSubMenu?.Invoke(SubMenuType.ITEMS);

		public void OpenSubMenu(SubMenuType subMenuType, List<string> subMenuButtonsName)
		{
			ClearSubMenu();

			foreach (var button in subMenuButtonsName)
			{
				ButtonUi buttonToAdd = new ButtonUi();
				buttonToAdd.Init(button);
				_subMenuContainer.AddChild(buttonToAdd);
			}
		}

		private void ClearSubMenu()
		{
			foreach (var child in _subMenuContainer.GetChildren())
				_subMenuContainer.RemoveChild(child);
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
			if (_subMenuContainerPath is null)
				Log.PrintWarning("SubMenuContainer Path missing");
		}

		public void OnMainButtonBattleMenuClicked()
		{

		}

		public override void _ExitTree()
		{
			Unsubscribe();
		}
	}
}