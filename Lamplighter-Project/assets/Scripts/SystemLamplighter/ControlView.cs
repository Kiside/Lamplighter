using Godot;
using SystemLamplighter;

namespace SystemLamplighter
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