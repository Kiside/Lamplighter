using Godot;
using MessagePipe;
using System;
using SystemLamplighter;

namespace SystemLamplighter.BattleMenu
{

	public partial class BattleMenuController : AbstractController<BattleMenuView, BattleMenuModel>
	{
		private IDisposable _disposable;
		readonly private ISubscriber<ActionEvent> _subscriber;
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			var bag = DisposableBag.CreateBuilder();

			_subscriber.Subscribe(ManageEvent).AddTo(bag);

			_disposable = bag.Build();
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{

		}

		private void ManageEvent(ActionEvent actionEvent)
		{

		}

		private void OpenSubMenu(SubMenuType subMenuType)
		{
			_view.OpenSubMenu(subMenuType, _model.Get(subMenuType));
		}

		public void Attack()
		{

		}

		public void Magic()
		{

		}

		public void Guard()
		{

		}

		public override void _ExitTree()
		{
			_disposable.Dispose();
		}
	}



}
