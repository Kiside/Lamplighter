using SystemLamplighter;
using Godot;
using System.Collections.Generic;
using System.Linq;
using Characters.Abstract;
using Characters.Loadout;
using SystemLamplighter.ATB;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.Abstract.MVC;
using SystemLamplighter.Interfaces;

namespace Characters.NPC;
/// <summary>
/// Classe Model per gli NPC
/// </summary>
public partial class NpCharacterModel : AbstractModel
{
	#region EXPORT PROPERTIES
	[Export]
	protected NodePath _combatLoadoutNode;
	[Export]
	protected AtbCharacterProperties _atbCharacterProperties;
	[Export]
	protected Godot.Collections.Array<GroupsName> _groups;
	#endregion

	#region PROTECTED PROPERTIES 
	protected AbstractCombat<NpCharacterController> _combat;
	protected AbstractMovement<NpCharacterController> _movement;
	protected CombatLoadout _combatLoadout;
	protected IActionData _currentAction;
	#endregion

	#region PUBLIC PROPERTIES
	public AbstractCombat<NpCharacterController> Combat { get => _combat; set => _combat = value; }
	public AbstractMovement<NpCharacterController> Movement { get => _movement; set => _movement = value; }
	public CombatLoadout CombatLoadout { get => _combatLoadout; set => _combatLoadout = value; }
	public IActionData CurrentAction { get => _currentAction; set => _currentAction = value; }
	public AtbCharacterProperties AtbCharacterProperties => _atbCharacterProperties;
	public List<string> Groups { get => _groups.Select(g => g.ToString()).ToList<string>(); }

	#endregion


	public override void Init()
	{
		NodeChecking();
	}

	private void NodeChecking()
	{
		// DebugLamplighter.Assert(_combatLoadoutNode != null, $"{noNode} CombatLoadout is null");
		// DebugLamplighter.Assert(_atbCharacterProperties != null, $"{noNode} AtbCharacterProperties is null");
		// DebugLamplighter.Assert(_battleMenuNode != null, $"{noNode} battleMenuNode is null");

		if (_combatLoadoutNode != null)
			_combatLoadout = GetNode<CombatLoadout>(_combatLoadoutNode);

	}


}