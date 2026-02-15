using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using SystemLamplighter;
using SystemLamplighter.BattleMenu;
using Characters.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MessagePipe;
using SystemLamplighter.Events;
using SystemLamplighter.Extensions;
using System.Linq;
using SystemLamplighter.DataStructure;
using Characters.Abstract;
using SystemLamplighter.Interfaces;
using SystemLamplighter.Setup;

namespace Characters.Playable
{
	/// <summary>
	/// Classe per il controller del personaggio
	/// </summary>
	public partial class PlayableCharacterController : CharacterController<PlayableCharacterView,PlayableCharacterModel>, 
	IHasCombatInterface<ICombatActor>
	{
		#region PUBLIC
		public ICombatActor CombatActor {get => _model.CombatActor;}
		public BattleMenuController BattleMenuController => _battleMenuController; 
		#endregion

		#region PROTECTED/PRIVATE PROPERTIES 
		protected AbstractCombat<PlayableCharacterController> _combat { get => _model.Combat; set => _model.Combat = value; }
		protected AbstractMovement<PlayableCharacterController> _movement { get => _model.Movement; set => _model.Movement = value; }
		protected BattleMenuController _battleMenuController { get => _model.BattleMenu;}
		protected List<string> _groups {get => _model.Groups;}
		private bool _lockOn = false;
		private readonly DisposableBagBuilder _bag = DisposableBag.CreateBuilder();
		protected IGroupsInitiator _groupsInitiator;
		#endregion
		
		#region PUBLIC PROPERTIES
		public bool LockOn { get { return _lockOn; } set { _lockOn = value; } }
		#endregion

		public override void _Ready()
		{
			base._Ready();
		}

		protected override void OnInit()
		{
			_groupsInitiator = new GroupsInitiator(_groups, this);

			_combat.Init(this);
			_movement.Init(this);

			Subscribe();
		}

		public ICombatActor GetCombatInterface() => CombatActor;

		#region Subscribe/Unsubscribe
		protected override void Subscribe()
		{
			
		}
		protected override void Unsubscribe()
		{
			_bag.Build().Dispose();
		}
		#endregion

		// TODO IL NODE CHECKING È DA CONTROLLARE BENE SE PUÒ ESSERE GENERALIZZATO ANCORA
		protected override void NodeChecking()
		{
			base.NodeChecking();

			string noNode = "There is no ";
			Debug.Assert(MovementNode != null, $"{noNode} MovementNode is null.");
			Debug.Assert(CombatNode != null, $"{noNode} CombatNode is null.");

			if (MovementNode != null)
				_movement = GetNode<AbstractMovement<PlayableCharacterController>>(MovementNode);

			if (CombatNode != null)
				_combat = GetNode<AbstractCombat<PlayableCharacterController>>(CombatNode);
		}

		public override void _PhysicsProcess(double delta)
		{
			_combat.Combat();
			Velocity = _movement.Move(delta);
			MoveAndSlide();
		}

		public override void _ExitTree()
		{
			Unsubscribe();
			// AtbProperties.Unsubscribe();
						
			base._ExitTree();
		}
	}
}