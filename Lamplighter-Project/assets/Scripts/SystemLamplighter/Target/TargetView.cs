using SystemLamplighter.Abstract.MVC;
using SystemLamplighter.DataStructure.GeneralData;
using SystemLamplighter.MVC;

namespace SystemLamplighter.Target;

/// <summary>
/// View per la gestione dei Target
/// </summary>
public partial class TargetView : AbstractView, ITargetView
{
	public override void Init()
	{
	
	}

	public void Render(TargetCursorState cursorState)
	{
		switch(cursorState)
		{
			case PositionCursorState positionCursorState:
				DrawShape(positionCursorState);
				break;
			case ActorCursorState actorCursorState:
				HighlightActor(actorCursorState);
				break;
		}
	}

	private void DrawShape(PositionCursorState positionCursorState) { }
	private void HighlightActor(ActorCursorState actorCursorState) { }
}