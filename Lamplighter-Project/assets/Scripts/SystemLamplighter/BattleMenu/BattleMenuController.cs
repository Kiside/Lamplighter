using Godot;
using MessagePipe;
using System;
using SystemLamplighter;

namespace SystemLamplighter.BattleMenu
{

	public partial class BattleMenuController : AbstractController<BattleMenuView, BattleMenuModel>
	{
		public bool Visible => _view.Visible;
		public void Show() => _view.Visible = true;
		public void Hide() => _view.Visible = false;

		

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			base._Ready();

			_view.OnActionClick += Action;
			_view.OnSubMenu += OpenSubMenu;
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{

		}

		private void OpenSubMenu(SubMenuType subMenuType)
		{
			_view.OpenSubMenu(subMenuType, _model.Get(subMenuType));
		}

		private void Action(string id)
		{
			Log.PrintMessage($"Action: {id}");
		}

		public override void _ExitTree()
		{
			Unsubscribe();
		}

		private void Unsubscribe()
		{
			_view.OnActionClick -= Action;
			_view.OnSubMenu -= OpenSubMenu;
		}
	}



}
