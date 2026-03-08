using System.Collections.Generic;
using System.Linq;
using Characters.Interfaces;
using Godot;
using LamplighterPlugins.CustomNodes;
using SystemLamplighter.DataStructure.GeneralData;
using SystemLamplighter.Debug;
using SystemLamplighter.Interfaces;

public partial class SelectionTargetRenderer : Control, ITargetRenderer
{
	[Export]
	Control _menuContainer;	
	[Export]
	PackedScene _selectionButton;
	[Export]
	NodePath _cameraPath;

	public bool IsInitialized => _selectionTargetsUi.Count > 0;

	private ActorCursorState _lastActorCursorState;

	Dictionary<ICombatActor, SelectionLabel> _selectionTargetsUi;
	ICombatActorPositionProvider<Node3D> _combatActorPositionProvider;
	ICombatActorHighlightableProvider _highlithableProvider;
	IHighlightSystem _highlightSystem;
	Camera3D _camera;

	public override void _Ready()
	{
		base._Ready();

		_selectionTargetsUi = new Dictionary<ICombatActor, SelectionLabel>();
		_camera = GetNode<Camera3D>(_cameraPath);
	}

	public void BootstrapInit(SelectionTargetRendererContext context)
	{
		_combatActorPositionProvider = context.combatActorPositionProvider;
		_highlithableProvider = context.highlightableProvider;
		_highlightSystem = context.highlightSystem;
	}

	public void InitMenu()
	{
		DebugLamplighter.Assert(_combatActorPositionProvider != null, "_combatActorPositionProvider is null");
		DebugLamplighter.Assert(_selectionButton != null, "_selectionButton is null");
		
		foreach(var a in _combatActorPositionProvider.GetActors())
		{
			var button = _selectionButton.Instantiate();
			_menuContainer.AddChild(button);

			if(button is SelectionLabel selectionLabel)
				_selectionTargetsUi.Add(a, selectionLabel);
			else 
				DebugLamplighter.Assert(true, "button is not a SelectionLabel");
		}
	}

	public bool CanRender(TargetCursorState targetCursorState)
	{
		if(targetCursorState is ActorCursorState)
			return true;
		
		return false;
	}

	public void Render(TargetCursorState targetCursorState)
	{
		if(targetCursorState is ActorCursorState state)
		{
			InitMenu();

			if(_selectionTargetsUi.TryGetValue(state.ActorSelected, out SelectionLabel selectionLabel))
			{
				if(selectionLabel.IsSelected)
					selectionLabel.Deselect();
				else
					selectionLabel.Select();
			}

			if(_lastActorCursorState != null && _lastActorCursorState.ActorSelected != state.ActorSelected)
				_highlightSystem.Unhighlight(_highlithableProvider.GetHighlightable(_lastActorCursorState.ActorSelected));	

			_camera.LookAt(_combatActorPositionProvider.GetPosition(state.ActorSelected));
			_highlightSystem.Highlight(_highlithableProvider.GetHighlightable(state.ActorSelected));
			_lastActorCursorState = state;
		}
	}

	public override void _ExitTree()
	{
		_selectionTargetsUi.Clear();

		_lastActorCursorState = null;

		base._ExitTree();
	}
}