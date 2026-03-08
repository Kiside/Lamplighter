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
public partial class TargetView : ControlView, IHighlighter, INodeOfGroup
{
	[Export]
	private NodePath _selectionTargetUiPath;
	[Export]
	/// <summary>
	/// - highlighter 
	/// </summary>
	protected Godot.Collections.Array<GroupsName> _groups;
	private IGroupsInitiator _groupsInitiator;
	private SelectionTargetRenderer _selectionTargetUi;
	private IHighlightSystem _highlightSystem;

	public override void Init()
	{
		CheckNodes();
	
		InitiateGroups();
	}

	public void InitiateGroups()
	{
		_groupsInitiator = new GroupsInitiator(_groups.Select(g => g.ToString()).ToList<string>(), this);
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
		if(!_selectionTargetUi.Visible)
			_selectionTargetUi.Visible = true;
		
		_selectionTargetUi.Render(actorCursorState);
		
	}
}