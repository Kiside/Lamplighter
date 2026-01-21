using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Characters.Inteaces;
using Characters.Interfaces;
using Godot;
using MessagePipe;
using SystemLamplighter;
using SystemLamplighter.Events;
using SystemLamplighter.Extensions;

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
		private readonly DisposableBagBuilder _bag = DisposableBag.CreateBuilder();
		

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
			if(ev.Actor != this)
				return;
			

			// Scelta dell'azione da fare 
			var random = new Godot.RandomNumberGenerator();
			random.Randomize();
			var choice = random.Randi() % 2 +1;

			float speedMultiplier = 1f;
			
			if(choice == 1)
			{
				speedMultiplier = _model.CombatLoadout.GetAttacksId()[0].ActionSpeedMultiplier;	
			}
			else
			{
				speedMultiplier = _model.CombatLoadout.GetMagicsId()[0].ActionSpeedMultiplier;	
			}
			
			// Fine scelta
			AtbProperties.EndCommandStatus(speedMultiplier);
			this.PublishEvent<AtbCommandPhaseEndEvent>(new AtbCommandPhaseEndEvent(this));
		}

		protected override void Subscribe()
		{
			this.SubscribeEvent<AtbExecuteActionEvent>(OnExecuteCombatAction).AddTo(_bag);
			this.SubscribeEvent<AtbCommandPhaseStartedEvent>(OnCommandPhaseStarted).AddTo(_bag);
		}
		protected override void Unsubscribe()
		{
			_bag.Build().Dispose();
		}

		public void OnExecuteCombatAction(AtbExecuteActionEvent ev)
		{
			if(ev.Actor != this)
				return;
			
			Log.PrintMessage("ESEGUO L'AZIONE");
			// Eseguo l'azione
			// Ad azione eseguita resetto la posizione del personaggio sull'ATB
			this.PublishEvent(new AtbEndExecuteActionEvent(this));
		}

		#region ICombatActor
		public AtbCharacterStatus UpdateAtbPosition(float value) => AtbProperties.UpdatePosition(value);
		#endregion
	}
	
}