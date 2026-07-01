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
using SystemLamplighter.ATB.Interfaces;
using SystemLamplighter.Area;

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
		/// <summary>
		/// Variabile che gestisce il modo in cui il personaggio combatte 
		/// </summary>
		public ICombatBrain CombatBrain => _combatBrain; 
		#endregion

		#region PROTECTED/PRIVATE PROPERTIES 
		protected AbstractCombat<LamplighterCharacterController> _combat { get => _model.Combat; set => _model.Combat = value; }
		protected AbstractMovement<LamplighterCharacterController> _movement { get => _model.Movement; set => _model.Movement = value; }
		protected ICombatBrain _combatBrain { get => _model.CombatBrain;}
		private bool _lockOn = false;

		private bool tweening = false;
		/// <summary>
		/// Variabile che si occupa della logica della risoluzione di "effetti" sul personaggio
		/// </summary>
		private IEffectResolver _effectResolver;
		/// <summary>
		/// Variabile che si occupa delle logiche di business riguardo l'Action Time Bar del personaggio
		/// </summary>
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

		// TODO: Metodo da cancellare? 
		private void SetIdCombatActor()
		{
			//_model.InitCombatActor(Id, _atbCharacterService);
		}

		// TODO: Metodo da cancellare?
		public ICombatActor GetCombatInterface() => CombatActor;

		#region Subscribe/Unsubscribe
		protected override void Subscribe()
		{
			_model.HurtBoxArea.AreaEntered += OnHurtBoxAreEntered;
		}
		protected override void Unsubscribe()
		{
			_bag.Build().Dispose();
			_atbCharacterService.Dispose();
		}
		#endregion

		/// <summary>
		/// Metodo chiamato quando entra un corpo nella "HurtBox"
		/// </summary>
		/// <param name="areaEntered"></param>
		protected void OnHurtBoxAreEntered(Area3D areaEntered)
		{
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

		/// <summary>
		/// Metodo chiamato dal bootstrap per inizializzare e passare gli oggetti che servono al personaggio
		/// </summary>
		/// <param name="effectResolver"></param>
		/// <param name="atbCharacterService"></param>
		public void BootstrapInit(IEffectResolver effectResolver, IAtbCharacterService atbCharacterService)
		{
			_effectResolver = effectResolver;
			_effectResolver.Init(_model.CharacterProperties);

			_atbCharacterService = atbCharacterService;
			_atbCharacterService.Init(_model.CharacterProperties, 
			this.GetSubscriber<AtbEndExecuteActionEvent>(),
			this.GetSubscriber<AtbCommandPhaseEndEvent>());

			CombatActor.Init(atbCharacterService);
		}

		public override void _PhysicsProcess(double delta)
		{
			//_combat.Combat();
			if(!_movement.Disable)
			{
				Velocity = _movement.RealtimeMove(delta);
				MoveAndSlide();
			}

			if(_movement.IsMovementFinished())
				return;
				
			Velocity = (Vector3) _movement.PointToPointMove(Vector3.Back, Vector3.Back, Id);;
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