using SystemLamplighter.DataStructure.GeneralData;

public interface ITargetRenderer
{
	public bool CanRender(TargetCursorState targetCursorState);
	public void Render(TargetCursorState targetCursorState);
}