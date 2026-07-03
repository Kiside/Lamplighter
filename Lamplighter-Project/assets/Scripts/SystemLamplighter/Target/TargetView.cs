using System.Linq;
using Godot;
using Microsoft.Extensions.DependencyInjection;
using SystemLamplighter.Abstract.MVC;
using SystemLamplighter.Bootstrap;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.DataStructure.EffectsData;
using SystemLamplighter.DataStructure.GeneralData;
using SystemLamplighter.Debug;
using SystemLamplighter.Interfaces;
using SystemLamplighter.MVC;
using SystemLamplighter.Setup;
using SystemLamplighter.Target.Interfaces;
using SystemLamplighter.Tool;

namespace SystemLamplighter.Target;

/// <summary>
/// View per la gestione dei Target
/// </summary>
public partial class TargetView : ControlView
{
	[Export]
	private NodePath _infoUiPath;
	[Export]
	private NodePath _infoLabelPath;
	[Export]
	private NodePath _selectionTargetUiPath;
	[Export]
	private NodePath _shapeTargetUiPath;

	[Export]
	/// <summary>
	/// - highlighter 
	/// </summary>
	protected Godot.Collections.Array<GroupsName> _groups;

	private Control _infoUi;
	private Label _infoLabel;

	private ITargetRenderer _currentTargetRenderer;	

	public override void _EnterTree()
	{
		base._EnterTree();
	}

	public override void Init()
	{
		CheckNodes();

		_infoUi.Visible = false;
	}

	public void CheckNodes()
	{
		DebugLamplighter.Assert(_selectionTargetUiPath != null, "_selectionTargetUiPath is null");
		//DebugLamplighter.Assert(_shapeTargetUiPath != null, "_shapeTargetUiPath is null");
		DebugLamplighter.Assert(_infoUiPath != null, "_infoUiPath is null");
		DebugLamplighter.Assert(_infoLabelPath != null, "_infoLabelPath is null");

		_infoUi = GetNode<Control>(_infoUiPath);
		_infoLabel = GetNode<Label>(_infoLabelPath);

		_currentTargetRenderer = GetNode<SelectionTargetRenderer>(_selectionTargetUiPath);
	}

	public void ShowInfo(IActionData data)
	{
		_infoUi.Visible = true;

		var targetData = data.TargetData;

		string second = string.Empty;
		string first = string.Empty;

		string numberOfTargets = (targetData.NumberOfTargets > 1) ? $"{targetData.NumberOfTargets} targets" : "a target";

		switch (targetData.TargetType)
		{
			case TargetType.SELECTION:
				first = $"Select {numberOfTargets} to";
				break;
			case TargetType.SELF:
				break;
			default:
				break;
		}

		switch(data.Effects.FirstOrDefault())
		{
			case HealData:
				Log.PrintMessage("heal");
				second = "heal";
				break;
			case DamageData: case DotDamageEffectData:
				Log.PrintMessage("damage");
				second = "damage";
				break;
			case StatModifierEffectData stateModifierData:
				Log.PrintMessage("stat modifier");
				second = stateModifierData.Kind == ModifierKind.DEBUFF ? "debuff" : "buff";
				break;
		}

		_infoLabel.Text = $"{data.Name}: {first} {second}";
	}

	public void ShapeTargetRender(PositionCursorState positionCursorState)
	{
		//_currentTargetRenderer = GetNode<ShapeTargetRenderer>(_shapeTargetUiPath);
	}
	public void SelectionTargetRender(ActorCursorState actorCursorState, TargetType targetType)
	{
		_currentTargetRenderer = GetNode<SelectionTargetRenderer>(_selectionTargetUiPath);
		
		if(_currentTargetRenderer.CanRender(actorCursorState))
			_currentTargetRenderer.Render(actorCursorState, targetType);
	}

	public void HideUi()
	{
		_currentTargetRenderer.EnableUi(false);
		_currentTargetRenderer = null;
		_infoUi.Visible = false;
	}
}