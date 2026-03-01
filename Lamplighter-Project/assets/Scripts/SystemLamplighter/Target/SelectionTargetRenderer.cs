using System.Collections.Generic;
using Characters.Interfaces;
using Godot;
using LamplighterPlugins.CustomNodes;
using SystemLamplighter.DataStructure.GeneralData;
using SystemLamplighter.Debug;

public partial class SelectionTargetRenderer : Control, ITargetRenderer
{
	[Export]
	Control _menuContainer;	
	[Export]
	PackedScene _selectionButton;

	public bool IsInitialized => _selectionTargetsUi.Count > 0;

	Dictionary<ICombatActor, SelectionLabel> _selectionTargetsUi;

	public override void _Ready()
	{
		base._Ready();

		_selectionTargetsUi = new Dictionary<ICombatActor, SelectionLabel>();
	}

	public void InitMenu(List<ICombatActor> actors)
	{
		foreach(var a in actors)
		{
			var button = _selectionButton.Instantiate();
			_menuContainer.AddChild(button);

			if(button is SelectionLabel selectionLabel)
				_selectionTargetsUi.Add(a, selectionLabel);
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
		
	}

	public override void _ExitTree()
	{
		_selectionTargetsUi.Clear();

		base._ExitTree();
	}
}