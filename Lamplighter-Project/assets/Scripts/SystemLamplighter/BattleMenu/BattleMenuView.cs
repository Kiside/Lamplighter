using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata;
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
			Debug.Assert(_attackButton != null, "_attackButton is null");
			_attackButton.OnClick += HandleClick;
			Debug.Assert(_magicButton != null, "_magicButton is null");
			_magicButton.OnClick += HandleClick;
			Debug.Assert(_guardButton != null, "_guardButton is null");
			_guardButton.OnClick += HandleClick;
			Debug.Assert(_itemButton != null, "_itemButton is null");
			_itemButton.OnClick += HandleClick;
		}

		private void Unsubscribe()
		{
			Debug.Assert(_attackButton != null, "_attackButton is null");
			_attackButton.OnClick -= HandleClick;
			Debug.Assert(_magicButton != null, "_magicButton is null");
			_magicButton.OnClick -= HandleClick;
			Debug.Assert(_guardButton != null, "_guardButton is null");
			_guardButton.OnClick -= HandleClick;
			Debug.Assert(_itemButton != null, "_itemButton is null");
			_itemButton.OnClick -= HandleClick;
		}

		private void HandleClick(string idButton)
		{
			Debug.Assert(idButton != null || idButton == String.Empty, "idButton is null or empty!");

			if (idButton == String.Empty)
				return;

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
					OnActionClick?.Invoke(idButton);
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

			Debug.Assert(subMenuButtonsName != null, "subMenuButtonsName is null");
			Debug.Assert(subMenuButtonsName.Count > 0, "subMenuButtonsName has 0 elements");

			foreach (var button in subMenuButtonsName)
			{
				ButtonUi buttonToAdd = new ButtonUi();
				buttonToAdd.Init(button);
				buttonToAdd.SetMinimumSize(new Vector2(251, 60));
				_subMenuContainer.AddChild(buttonToAdd);
				buttonToAdd.OnClick += HandleClick;
			}
		}

		private void ClearSubMenu()
		{
			Debug.Assert(_subMenuContainer != null, "There is no SubMenuContainer");

			if (_subMenuContainer.GetChildCount() <= 0)
				return;

			foreach (var child in _subMenuContainer.GetChildren())
				_subMenuContainer.RemoveChild(child);
		}

		private void NodeChecking()
		{
			Debug.Assert(_attackButton != null, "_attackButton is null");
			Debug.Assert(_magicButton != null, "_magicButton is null");
			Debug.Assert(_guardButton != null, "_guardButton is null");
			Debug.Assert(_itemButton != null, "_itemButton is null");
		}

		public override void _ExitTree()
		{
			Unsubscribe();
			UnsubscrieSubMenu();
		}

		private void UnsubscrieSubMenu()
		{
			Debug.Assert(_subMenuContainer != null, "There is no _subMenuContainer");

			if (_subMenuContainer.GetChildCount() > 0)
			{
				foreach (var child in _subMenuContainer.GetChildren())
				{
					if (child is ButtonUi button)
					{
						button.OnClick -= HandleClick;
					}
				}
			}
		}
	}
}