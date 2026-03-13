using System.Linq;
using Godot;
using Microsoft.Extensions.DependencyInjection;
using SystemLamplighter.Abstract.MVC;
using SystemLamplighter.Bootstrap;
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
public partial class TargetView : ControlView, IHighlighter
{
	[Export]
	private NodePath _selectionTargetUiPath;
	[Export]
	/// <summary>
	/// - highlighter 
	/// </summary>
	protected Godot.Collections.Array<GroupsName> _groups;
	private SelectionTargetRenderer _selectionTargetUi;
	private IHighlightSystem _highlightSystem;

	public override void _EnterTree()
	{
		base._EnterTree();
	}

	public override void Init()
	{
		CheckNodes();
	}

	public void InitHighlighterSystem(IHighlightSystem highlightSystem)
	{
		_highlightSystem = highlightSystem;
	}

	public void CheckNodes()
	{
		DebugLamplighter.Assert(_selectionTargetUiPath != null, "_selectionTargetUiPath is null");

		_selectionTargetUi = GetNode<SelectionTargetRenderer>(_selectionTargetUiPath);
	}



	public void ShapeTargetRender(PositionCursorState positionCursorState)
	{
		
	}
	public void SelectionTargetRender(ActorCursorState actorCursorState)
	{
		// if(!_selectionTargetUi.Visible)
		// 	_selectionTargetUi.Visible = true;
		
		if(_selectionTargetUi.CanRender(actorCursorState))
			_selectionTargetUi.Render(actorCursorState);
		
	}
}