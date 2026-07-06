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
using SystemLamplighter.Providers;
using SystemLamplighter.Target.Interfaces;
using SystemLamplighter.Tool;

namespace SystemLamplighter.Target;

/// <summary>
/// Classe che renderizza l'estetica della selezione di un target
/// </summary>
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
		EnableUi(false);
		//_camera = GetNode<Camera3D>(_cameraPath);
	}

	public void BootstrapInit(SelectionTargetRendererContext context)
	{
		_targetableProvider = context.TargetableProvider;
	}

	public void InitMenu(ActorCursorState state, TargetType targetType)
	{
		DebugLamplighter.Assert(_targetableProvider != null, "_targetableProvider is null");
		DebugLamplighter.Assert(_selectionButton != null, "_selectionButton is null");
		
		EnableUi(true);

		if(targetType == TargetType.SELF && _menuContainer.GetChildCount() == 0)
		{
			CreateButton(state.TargetableFocused);
		}
		else if(targetType == TargetType.SELECTION)
		{
			var targetables =  _targetableProvider.GetTargetables(state.WhoTarget);

			if(_menuContainer.GetChildCount() != targetables.Count)
			{
				Clean();

				foreach(var targetable in targetables)
				{
					CreateButton(targetable);
				}
			}
		}
	}

	private void CreateButton(ITargetable targetable)
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

	public bool CanRender(TargetCursorState targetCursorState)
	{
		if(targetCursorState is ActorCursorState)
			return true;
		
		return false;
	}

	public void Render(TargetCursorState targetCursorState, TargetType targetType)
	{
		Log.PrintMessage($"ENTRO");
		if(targetCursorState is ActorCursorState actorCursorState)
		{
			InitMenu(actorCursorState, targetType);
			
			foreach(var element in _selectionTargetsUiDictionary)
			{
				GD.Print("");
				var targetable = element.Key;
				var selectionLabel = element.Value;

				if(actorCursorState.TargetablesSelected.Count >= 0)
				{
					Log.PrintMessage($"{actorCursorState.TargetablesSelected.Count}");
					if(actorCursorState.TargetablesSelected.Contains(targetable))
					{
						Log.PrintMessage("SELECT");
						selectionLabel.Select();
					}
					else
					{
						Log.PrintMessage("DESELECT");
						selectionLabel.Deselect();
					}
				}

				if(targetable == actorCursorState.TargetableFocused)
					selectionLabel.Focus();
				else
					selectionLabel.Unfocus();
			}
		}
	}

	private void Clean()
	{
		_menuContainer.CleanChildren();
		_selectionTargetsUiDictionary.Clear();
	}

	public void EnableUi(bool value) => _ui.Visible = value;

	public override void _ExitTree()
	{
		Clean();

		_lastActorCursorState = null;

		base._ExitTree();
	}
}