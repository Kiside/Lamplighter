using System.Linq;
using Godot;
using SystemLamplighter.Abstract.MVC;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.DataStructure.GeneralData;
using SystemLamplighter.Debug;
using SystemLamplighter.Interfaces;
using SystemLamplighter.MVC;
using SystemLamplighter.Setup;

namespace SystemLamplighter.Target;

/// <summary>
/// View per la gestione dei Target
/// </summary>
public partial class TargetView : ControlView, ITargetView, IHighlighter
{
	[Export]
	private NodePath _selectionTargetUiPath;
	[Export]
	/// <summary>
	/// - highlighter 
	/// </summary>
	protected Godot.Collections.Array<GroupsName> _groups;

	private IGroupsInitiator _groupsInitiator;

	private Control _selectionTargetUi;
	private IHighlightSystem _highlightSystem;

	public override void Init()
	{
		CheckNodes();
	
		_groupsInitiator = new GroupsInitiator(_groups.Select(g => g.ToString()).ToList<string>(), this);
	}

	public void InitHighlighterSystem(IHighlightSystem highlightSystem)
	{
		_highlightSystem = highlightSystem;
	}

	public void CheckNodes()
	{
		DebugLamplighter.Assert(_selectionTargetUiPath != null, "_selectionTargetUiPath is null");

		_selectionTargetUi = GetNode<Control>(_selectionTargetUiPath);
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
	private void HighlightActor(ActorCursorState actorCursorState)
	{
		if(!_selectionTargetUi.Visible)
			_selectionTargetUi.Visible = true;
		
		
		
	}
}