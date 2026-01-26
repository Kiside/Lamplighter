using Godot;
using MessagePipe;
using System;
using System.Collections.Generic;
using System.Linq;
using SystemLamplighter;
using SystemLamplighter.Debug;
using SystemLamplighter.Events;

namespace SystemLamplighter.BattleMenu
{

	public partial class BattleMenuController : AbstractController<BattleMenuView, BattleMenuModel>
	{
		public bool Visible => _view.Visible;
		public void Show() => _view.Visible = true;
		public void Hide() => _view.Visible = false;

		private List<ISubMenuDefinition> Menus => _model._menus;

		public event Action<ISubMenuDefinition> OnOpenSubMenu;
		public event Action<IActionData> OnActionClick;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			base._Ready();

			_view.OnActionClick += Action;
			_view.OnSubMenu += RequestOpenSubMenu;
			_view.BuildMenu(Menus);

			if(_model.startHide)
				Hide();
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{

		}

		private void RequestOpenSubMenu(string subMenu)
		{
			DebugLamplighter.Assert(Menus != null, "There is no Menus");
			DebugLamplighter.Assert(Menus.Count > 0, "There is no element in Menus");
			
			OnOpenSubMenu?.Invoke(Menus.First(m => m.Id == subMenu));
		}

		public void OpenSubMenu(IReadOnlyList<IActionData> subMenuButtonNames)
		{
			_view.OpenSubMenu(subMenuButtonNames);
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
