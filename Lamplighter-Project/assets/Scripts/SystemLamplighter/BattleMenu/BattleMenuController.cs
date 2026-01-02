using Godot;
using MessagePipe;
using System;
using System.Collections.Generic;
using SystemLamplighter;
using SystemLamplighter.Events;

namespace SystemLamplighter.BattleMenu
{

	public partial class BattleMenuController : AbstractController<BattleMenuView, BattleMenuModel>
	{
		public bool Visible => _view.Visible;
		public void Show() => _view.Visible = true;
		public void Hide() => _view.Visible = false;

		public event Action<SubMenuType> OnOpenSubMenu;
		public event Action<IActionData> OnActionClick;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			base._Ready();

			_view.OnActionClick += Action;
			_view.OnSubMenu += RequestOpenSubMenu;

			if(_model.startHide)
				Hide();
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{

		}

		private void RequestOpenSubMenu(SubMenuType subMenuType)
		{
			OnOpenSubMenu?.Invoke(subMenuType);
		}

		public void OpenSubMenu(SubMenuType subMenuType, List<IActionData> subMenuButtonNames)
		{
			_view.OpenSubMenu(subMenuType, subMenuButtonNames);
		}

		private void Action(IActionData actionData)
		{
			Hide();
			OnActionClick?.Invoke(actionData);
		}

		public override void _ExitTree()
		{
			Unsubscribe();
		}

		private void Unsubscribe()
		{
			_view.OnActionClick -= Action;
			_view.OnSubMenu -= RequestOpenSubMenu;
		}
	}



}
