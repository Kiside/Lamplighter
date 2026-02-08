using Godot;
using SystemLamplighter;
using SystemLamplighter.Abstract.MVC;

namespace SystemLamplighter.MVC
{
	public abstract partial class ControlView : AbstractView
	{
		[Export]
		protected Control _control;

		public override bool Visible
		{
			get => _control.Visible;
			set => _control.Visible = value;
		}
		
	}
}