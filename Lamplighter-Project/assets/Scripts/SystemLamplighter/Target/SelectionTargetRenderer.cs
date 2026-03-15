using System.Collections.Generic;
using System.Linq;
using Characters.Interfaces;
using Godot;
using LamplighterPlugins.CustomNodes;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.DataStructure.GeneralData;
using SystemLamplighter.Debug;
using SystemLamplighter.Extensions;
using SystemLamplighter.Interfaces;
using SystemLamplighter.Tool;

public partial class SelectionTargetRenderer : Control, ITargetRenderer
{
	[Export]
	Control _ui;
	[Export]
	Control _menuContainer;	
	[Export]
	PackedScene _selectionButton;
	[Export]
	NodePath _cameraPath;

	public bool IsInitialized => _selectionTargetsUi.Count > 0;

	private ActorCursorState _lastActorCursorState;

	Dictionary<ITargetable, SelectionLabel> _selectionTargetsUi;
	ITargetableProvider _targetableProvider;
	Camera3D _camera;

	public override void _Ready()
	{
		base._Ready();

		_selectionTargetsUi = new Dictionary<ITargetable, SelectionLabel>();
		_ui.Visible = false;
		//_camera = GetNode<Camera3D>(_cameraPath);
	}

	public void BootstrapInit(SelectionTargetRendererContext context)
	{
		_targetableProvider = context.TargetableProvider;
	}

	public void InitMenu()
	{
		DebugLamplighter.Assert(_targetableProvider != null, "_targetableProvider is null");
		DebugLamplighter.Assert(_selectionButton != null, "_selectionButton is null");
		
		_ui.Visible = true;
		var targetables =  _targetableProvider.GetTargetables();

		if(_menuContainer.GetChildCount() != targetables.Count)
		{
			_menuContainer.CleanChildren();
			_selectionTargetsUi.Clear();

			foreach(var targetable in _targetableProvider.GetTargetables())
			{
				var button = _selectionButton.Instantiate();
				_menuContainer.AddChild(button);

				if(button is SelectionLabel selectionLabel)
				{
					selectionLabel.SetLabelText(targetable.TargetableName);
					_selectionTargetsUi.TryAdd(targetable, selectionLabel);
				}
				else 
					DebugLamplighter.Assert(true, "button is not a SelectionLabel");
			}
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

			if(_selectionTargetsUi.TryGetValue(state.TargetableSelected, out SelectionLabel selectionLabel))
			{
				if(selectionLabel.IsSelected)
					selectionLabel.Unfocus();
				else
					selectionLabel.Focus();
			}

			if(_lastActorCursorState != null && state != null)
				Log.PrintMessage($"LAST CURSOR:{_lastActorCursorState.TargetableSelected.TargetableName} - CURRENT CURSOR: {state.TargetableSelected.TargetableName}");
			if(_lastActorCursorState != null && _lastActorCursorState.TargetableSelected != state.TargetableSelected)
			{
				
				_lastActorCursorState.TargetableSelected.Deselect();
				_selectionTargetsUi.TryGetValue(_lastActorCursorState.TargetableSelected, out SelectionLabel lastSelectionLabel);
				Log.PrintMessage($"LAST SELECTION LABEL:{lastSelectionLabel.Label}");
				lastSelectionLabel?.Unfocus();
			}


			//_camera.LookAt(_combatActorPositionProvider.GetPosition(state.ActorSelected));
			state.TargetableSelected.Select();

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