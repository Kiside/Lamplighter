using System.Collections.Generic;
using Characters.Inteaces;
using Characters.Interfaces;
using SystemLamplighter;
using SystemLamplighter.Events;

namespace Characters.NPC
{
	public partial class NpCharacterController : CharacterController<NpCharacterView, NpCharacterModel>,
	ICombatActor, ICombatCommandHandler, ICombatActionExecutor
	{
		#region PUBLIC
		public AtbCharacterProperties AtbProperties => _model.AtbCharacterProperties;
		public CombatLoadout CombatLoadout { get => _model.CombatLoadout; set => _model.CombatLoadout = value; }
		public IActionData CurrentAction {get => _model.CurrentAction; set => _model.CurrentAction = value; }
		public AtbCharacterStatus AtbStatus => _model.AtbCharacterProperties.Status;
		#endregion


		protected AbstractCombat<NpCharacterController> _combat { get => _model.Combat; set => _model.Combat = value; }
		protected AbstractMovement<NpCharacterController> _movement { get => _model.Movement; set => _model.Movement = value; }
		protected List<string> _groups {get => _model.Groups;}

		public override void _Ready()
		{
			base._Ready();
		}

		protected override void OnInit()
		{
			_combat.Init(this);
			_movement.Init(this);

			Subscribe();
			GroupsInit();
		}
		protected void GroupsInit()
		{
			if(_groups == null || _groups.Count <= 0)
			{
				Log.PrintMessage("There are no groups");
				return;
			}

			foreach(var group in _groups)
			{
				AddToGroup(group);
				
			}
		}

		protected override void NodeChecking()
		{
			base.NodeChecking();
			
			if (MovementNode != null)
				_movement = GetNode<AbstractMovement<NpCharacterController>>(MovementNode);

			if (CombatNode != null)
				_combat = GetNode<AbstractCombat<NpCharacterController>>(CombatNode);
		}
	
		public void OnCommandPhaseStarted(AtbCommandPhaseStartedEvent ev)
		{
			
		}

		public void OnExecuteCombatAction(AtbExecuteActionEvent ev)
		{
			
		}

		#region ICombatActor
		public AtbCharacterStatus UpdateAtbPosition(float value) => AtbProperties.UpdatePosition(value);
		#endregion
	}
	
}