using SystemLamplighter.Common.Enums;
using SystemLamplighter.DataStructure.GeneralData;

namespace SystemLamplighter.Target.Interfaces;

public interface ITargetRenderer
{
	public void EnableUi(bool value);
	public bool CanRender(TargetCursorState targetCursorState);
	public void Render(TargetCursorState targetCursorState, TargetType targetType);
}