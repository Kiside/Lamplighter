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
using SystemLamplighter.Tool;

namespace Characters.Playable
{
	/// <summary>
	/// Classe per il controller del personaggio
	/// </summary>
	public partial class LamplighterCharacterController : CharacterController<LamplighterCharacterView,LamplighterCharacterModel>, 
	IHasCombatInterface<ICombatActor>
	{
		#region PUBLIC
		public ICombatActor CombatActor {get => _model.CombatActor;}
		public ICombatBrain CombatBrain => _combatBrain; 
		#endregion

		#region PROTECTED/PRIVATE PROPERTIES 
		protected AbstractCombat<LamplighterCharacterController> _combat { get => _model.Combat; set => _model.Combat = value; }
		protected AbstractMovement<LamplighterCharacterController> _movement { get => _model.Movement; set => _model.Movement = value; }
		protected ICombatBrain _combatBrain { get => _model.CombatBrain;}
		private bool _lockOn = false;

		private bool tweening = false;
		private IEffectResolver _effectResolver;
		private IAtbCharacterService _atbCharacterService;

		private readonly DisposableBagBuilder _bag = DisposableBag.CreateBuilder();
		#endregion
		
		#region PUBLIC PROPERTIES
		public bool LockOn { get { return _lockOn; } set { _lockOn = value; } }
		#endregion

		public override void _EnterTree()
		{
			base._EnterTree();
		}

		public override void _Ready()
		{
			base._Ready();
		}

		protected override void OnInit()
		{
			SetIdCombatActor();

			_combat.Init(this);
			_movement.Init(this);

			Subscribe();
		}

		private void SetIdCombatActor()
		{
			_model.InitCombatActor(Id, _atbCharacterService);
		}

		public ICombatActor GetCombatInterface() => CombatActor;

		#region Subscribe/Unsubscribe
		protected override void Subscribe()
		{
			_model.HurtBoxArea.AreaEntered += OnHurtBoxAreEntered;
		}
		protected override void Unsubscribe()
		{
			_bag.Build().Dispose();
		}
		#endregion

		protected void OnHurtBoxAreEntered(Area3D areaEntered)
		{
			Log.PrintMessage("Entrato!!!!");
			if(areaEntered.IsInGroup("hitboxarea") && areaEntered is HitBoxArea hitBoxArea)
				_effectResolver.Resolve(hitBoxArea.ActionData);
		}

		// TODO IL NODE CHECKING È DA CONTROLLARE BENE SE PUÒ ESSERE GENERALIZZATO ANCORA
		protected override void NodeChecking()
		{
			base.NodeChecking();

			string noNode = "There is no ";
			Debug.Assert(MovementNode != null, $"{noNode} MovementNode is null.");
			Debug.Assert(CombatNode != null, $"{noNode} CombatNode is null.");

			if (MovementNode != null)
				_movement = GetNode<AbstractMovement<LamplighterCharacterController>>(MovementNode);

			if (CombatNode != null)
				_combat = GetNode<AbstractCombat<LamplighterCharacterController>>(CombatNode);
		}

		public void BootstrapInit(IEffectResolver effectResolver, IAtbCharacterService atbCharacterService)
		{
			_effectResolver = effectResolver;
			_effectResolver.Init(_model.CharacterProperties);

			_atbCharacterService = atbCharacterService;
			_atbCharacterService.Init(_model.CharacterProperties);
		}

		public override void _PhysicsProcess(double delta)
		{
			_combat.Combat();
			if(!_movement.Disable)
			{
				Velocity = _movement.RealtimeMove(delta);
				MoveAndSlide();
			}

			if(_movement.IsMovementFinished())
				return;
				
			Velocity = (Vector3) _movement.PointToPointMove(Vector3.Back, Vector3.Back, Id);;
			MoveAndSlide();
			// var movement = _movement.PointToPointMove(Vector3.Back, Vector3.Back, Id);
			
			// if(movement != null && movement != Vector3.Zero)
			// {
			// 	Velocity = (Vector3) movement;
			// 	MoveAndSlide();
			// }
				
		}

		private void Tween(Godot.Vector3? movement)
		{
			if(tweening)
				return;

			Log.PrintMessage($"Start tween");
			tweening = true;
			var tween = CreateTween();
				tween.TweenProperty(this, "position", (Godot.Vector3)movement, 1.0f);
				tween.TweenCallback(Callable.From(this.QueueFree));

			
			tweening = false;
		}

		public override void _ExitTree()
		{
			Unsubscribe();
			// AtbProperties.Unsubscribe();
						
			base._ExitTree();
		}
	}
}