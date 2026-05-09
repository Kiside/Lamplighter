using Godot;
using MessagePipe;
using System;
using System.Collections.Generic;
using System.Linq;
using SystemLamplighter;
using SystemLamplighter.Debug;
using SystemLamplighter.Events;
using SystemLamplighter.Interfaces;
using SystemLamplighter.Abstract.MVC;
using SystemLamplighter.Tool;


namespace SystemLamplighter.BattleMenu
{
	/// <summary>
	/// Controller del menu di battaglia
	/// </summary>
	public partial class BattleMenuController : AbstractController<BattleMenuView, BattleMenuModel>, ICombatBrain
	{
		public bool Visible => _view.Visible;
		public void TurnOn() => _view.Visible = true;
		public void TurnOff() => _view.Visible = false;

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
				TurnOff();
		}

		private void RequestOpenSubMenu(string subMenu)
		{
			DebugLamplighter.Assert(Menus != null, "There is no Menus");
			DebugLamplighter.Assert(Menus.Count > 0, "There is no element in Menus");
			
			Log.PrintMessage("RequestOpenSubMenu: " + subMenu);

			OnOpenSubMenu?.Invoke(Menus.First(m => m.Id == subMenu));		
		}

		public void OpenSubMenu(IReadOnlyList<IActionData> subMenuButtonNames)
		{
			_view.OpenSubMenu(subMenuButtonNames);
		}

		public void Action(IActionData actionData)
		{
			TurnOff();
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
