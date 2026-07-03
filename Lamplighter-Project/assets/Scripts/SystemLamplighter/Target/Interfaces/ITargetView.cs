using SystemLamplighter.DataStructure.GeneralData;

namespace SystemLamplighter.Target.Interfaces;


public interface ITargetView
{
	public void Render(TargetCursorState cursorState);
}