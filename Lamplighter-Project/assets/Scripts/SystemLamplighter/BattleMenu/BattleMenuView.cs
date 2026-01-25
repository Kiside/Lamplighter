using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using SystemLamplighter;
using SystemLamplighter.DataStructure;
using SystemLamplighter.Debug;

namespace SystemLamplighter.BattleMenu
{
	public partial class BattleMenuView : ControlView
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

		public event Action<string> OnSubMenu;
		public event Action<IActionData> OnActionClick;

		public List<IActionData> SubMenuButtons {get; private set;}

		public override void Init()
		{
			NodeCheckingPath();

			if(SubMenuButtons == null)
				SubMenuButtons = new List<IActionData>();

			GetNodes();
			NodeChecking();
			Subscribe();
		}

		public void BuildMenu(List<ISubMenuDefinition> _menus)
		{
			_attackButton.Name = _menus.First(m => m.Id == SubMenuType.ATTACK.ToString()).Id;
			_attackButton.Text = _menus.First(m => m.Id == SubMenuType.ATTACK.ToString()).DisplayName;
			
			_magicButton.Name = _menus.First(m => m.Id == SubMenuType.MAGIC.ToString()).Id;
			_magicButton.Text = _menus.First(m => m.Id == SubMenuType.MAGIC.ToString()).DisplayName;
			
			_guardButton.Name = _menus.First(m => m.Id == SubMenuType.DEFEND.ToString()).Id;
			_guardButton.Text = _menus.First(m => m.Id == SubMenuType.DEFEND.ToString()).DisplayName;
			
			_itemButton.Name = _menus.First(m => m.Id == SubMenuType.ITEMS.ToString()).Id;
			_itemButton.Text = _menus.First(m => m.Id == SubMenuType.ITEMS.ToString()).DisplayName;
			
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
			DebugLamplighter.Assert(_attackButton != null, "_attackButton is null");
			_attackButton.OnClick += HandleClick;
			DebugLamplighter.Assert(_magicButton != null, "_magicButton is null");
			_magicButton.OnClick += HandleClick;
			DebugLamplighter.Assert(_guardButton != null, "_guardButton is null");
			_guardButton.OnClick += HandleClick;
			DebugLamplighter.Assert(_itemButton != null, "_itemButton is null");
			_itemButton.OnClick += HandleClick;
		}

		private void Unsubscribe()
		{
			DebugLamplighter.Assert(_attackButton != null, "_attackButton is null");
			_attackButton.OnClick -= HandleClick;
			DebugLamplighter.Assert(_magicButton != null, "_magicButton is null");
			_magicButton.OnClick -= HandleClick;
			DebugLamplighter.Assert(_guardButton != null, "_guardButton is null");
			_guardButton.OnClick -= HandleClick;
			DebugLamplighter.Assert(_itemButton != null, "_itemButton is null");
			_itemButton.OnClick -= HandleClick;
		}

		private void HandleClick(string idButton)
		{
			DebugLamplighter.Assert(idButton != null || idButton == String.Empty, "idButton is null or empty!");

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
					ActionButtonHandle(idButton);
					break;
			}
		}

		private void AttackHandle() => OnSubMenu?.Invoke(SubMenuType.ATTACK);
		private void MagicButtonHandle() => OnSubMenu?.Invoke(SubMenuType.MAGIC);
		private void GuardButtonHandle(string idButton) => OnSubMenu?.Invoke(SubMenuType.DEFEND);
		private void ItemButtonHandle() => OnSubMenu?.Invoke(SubMenuType.ITEMS);

		
		public void ActionButtonHandle(string id, IActionData actionD = null)
		{
			DebugLamplighter.Assert(SubMenuButtons != null, "SubMenuButtons is null");
			DebugLamplighter.Assert(id != string.Empty, "id string is empty");

			IActionData actionData;
			if(actionD == null)
				actionData = SubMenuButtons.Find(s => s.Name == id);
			else
				actionData = actionD;

			DebugLamplighter.Assert(actionData != null, "actionData is null, no action finded");
			
			OnActionClick?.Invoke(actionData);
		}
		
		/// <summary>
		/// Metodo per l'apertura del sub menu delle azioni/oggetti posseduti dal personaggio
		/// </summary>
		/// <param name="subMenuType"></param>
		/// <param name="subMenuButtonsName"></param>
		public void OpenSubMenu(SubMenuType subMenuType, List<IActionData> subMenuButtonsName)
		{
			ClearSubMenu();
			if(SubMenuButtons.Count > 0)
				SubMenuButtons.Clear();

			DebugLamplighter.Assert(SubMenuButtons != null, "SubMeneuButtons is null");
			DebugLamplighter.Assert(subMenuButtonsName != null, "subMenuButtonsName is null");
			DebugLamplighter.Assert(subMenuButtonsName.Count > 0, "subMenuButtonsName has 0 elements");

			foreach (var button in subMenuButtonsName)
			{
				SubMenuButtons.Add(button);
				ButtonUi buttonToAdd = new ButtonUi();
				buttonToAdd.Init(button.Name);
				buttonToAdd.SetMinimumSize(new Vector2(251, 60));
				_subMenuContainer.AddChild(buttonToAdd);
				buttonToAdd.OnClick += HandleClick;
			}
		}

		private void ClearSubMenu()
		{
			DebugLamplighter.Assert(_subMenuContainer != null, "There is no SubMenuContainer");

			if (_subMenuContainer.GetChildCount() <= 0)
				return;

			foreach (var child in _subMenuContainer.GetChildren())
				_subMenuContainer.RemoveChild(child);
		}

		private void NodeCheckingPath()
		{
			DebugLamplighter.Assert(_attackButtonPath != null, "_attackButtonPath is null");
			DebugLamplighter.Assert(_magicButtonPath != null, "_magicButtonPath is null");
			DebugLamplighter.Assert(_guardButtonPath != null, "_guardButtonPath is null");
			DebugLamplighter.Assert(_itemButtonPath != null, "_itemButtonPath is null");
		}

		private void NodeChecking()
		{
			DebugLamplighter.Assert(_attackButton != null, "_attackButton is null");
			DebugLamplighter.Assert(_magicButton != null, "_magicButton is null");
			DebugLamplighter.Assert(_guardButton != null, "_guardButton is null");
			DebugLamplighter.Assert(_itemButton != null, "_itemButton is null");
		}

		public override void _ExitTree()
		{
			Unsubscribe();
			UnsubscrieSubMenu();
		}

		private void UnsubscrieSubMenu()
		{
			DebugLamplighter.Assert(_subMenuContainer != null, "There is no _subMenuContainer");

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