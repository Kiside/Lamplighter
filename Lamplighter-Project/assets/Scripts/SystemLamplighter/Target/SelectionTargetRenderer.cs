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

	public bool IsInitialized => _selectionTargetsUiDictionary.Count > 0;

	private ActorCursorState _lastActorCursorState;

	Dictionary<ITargetable, SelectionLabel> _selectionTargetsUiDictionary;
	ITargetableProvider _targetableProvider;
	Camera3D _camera;

	public override void _Ready()
	{
		base._Ready();

		_selectionTargetsUiDictionary = new Dictionary<ITargetable, SelectionLabel>();
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
			_selectionTargetsUiDictionary.Clear();

			foreach(var targetable in _targetableProvider.GetTargetables())
			{
				var button = _selectionButton.Instantiate();
				_menuContainer.AddChild(button);

				if(button is SelectionLabel selectionLabel)
				{
					selectionLabel.SetLabelText(targetable.TargetableName);
					_selectionTargetsUiDictionary.TryAdd(targetable, selectionLabel);
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

			// if(_selectionTargetsUi.TryGetValue(state.TargetableFocused, out SelectionLabel selectionLabel))
			// {
			// 	if(selectionLabel.IsSelected)
			// 		selectionLabel.Unfocus();
			// 	else
			// 		selectionLabel.Focus();
			// }

			
			foreach(var element in _selectionTargetsUiDictionary)
			{
				var targetable = element.Key;
				var selectionLabel = element.Value;

				if(state.TargetablesSelected.Count > 0)
				{
					if(state.TargetablesSelected.Contains(targetable))
					{
						selectionLabel.Select();
					}
					else
					{
						selectionLabel.Deselect();
					}
				}

				if(targetable == state.TargetableFocused)
					selectionLabel.Focus();
				else
					selectionLabel.Unfocus();
			}

			// if(_lastActorCursorState != null && state != null)
			// 	Log.PrintMessage($"LAST CURSOR:{_lastActorCursorState.TargetableFocused.TargetableName} - CURRENT CURSOR: {state.TargetableFocused.TargetableName}");
			// if(_lastActorCursorState != null && _lastActorCursorState.TargetableFocused != state.TargetableFocused)
			// {
				
			// 	_lastActorCursorState.TargetableFocused.Deselect();
			// 	_selectionTargetsUiDictionary.TryGetValue(_lastActorCursorState.TargetableFocused, out SelectionLabel lastSelectionLabel);
			// 	Log.PrintMessage($"LAST SELECTION LABEL:{lastSelectionLabel.Label}");
			// 	lastSelectionLabel?.Unfocus();
			// }


			// //_camera.LookAt(_combatActorPositionProvider.GetPosition(state.ActorSelected));
			// state.TargetableFocused.Select();

			// _lastActorCursorState = state;
		}
	}

	public override void _ExitTree()
	{
		_selectionTargetsUiDictionary.Clear();

		_lastActorCursorState = null;

		base._ExitTree();
	}
}