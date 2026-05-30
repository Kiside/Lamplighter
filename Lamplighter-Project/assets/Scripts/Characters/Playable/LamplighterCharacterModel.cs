using Characters.Interfaces;
using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SystemLamplighter;
using SystemLamplighter.BattleMenu;
using SystemLamplighter.Debug;
using Characters.Abstract;
using Characters.Loadout;
using SystemLamplighter.ATB;
using SystemLamplighter.Abstract.MVC;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.Interfaces;
using SystemLamplighter.Combat.Actor;
using SystemLamplighter.Tool;

namespace Characters.Playable
{
	/// <summary>
	/// Classe Model per il personaggio
	/// </summary>
	public partial class LamplighterCharacterModel : AbstractModel
	{
		#region EXPORT PROPERTIES
		[Export]
		protected NodePath _combatLoadoutNode;
		[Export]
		protected AtbCharacterProperties _atbCharacterProperties;
		[Export]
		protected CharacterProperties _characterProperties;
		[Export]
		protected NodePath _combatBrainNode;
		[Export]
		protected NodePath _hurtBoxAreaNode;
		#endregion

		#region PROTECTED PROPERTIES 
		protected AbstractCombat<LamplighterCharacterController> _combat;
		protected AbstractMovement<LamplighterCharacterController> _movement;

		protected ICombatBrain _combatBrain;

		protected CombatLoadout _combatLoadout;

		protected IActionData _currentAction;

		protected ICombatActor _combatActor;
		protected Area3D _hurtBoxArea;
		private bool _lockOn = false;
		#endregion

		#region PUBLIC PROPERTIES
		public AbstractCombat<LamplighterCharacterController> Combat { get => _combat; set => _combat = value; }
		public AbstractMovement<LamplighterCharacterController> Movement { get => _movement; set => _movement = value; }
		public ICombatActor CombatActor => _combatActor;
		public ICombatBrain CombatBrain {get => _combatBrain;}
		public CombatLoadout CombatLoadout { get => _combatLoadout; set => _combatLoadout = value; }
		public NodePath CombatLoadoutNode {get => _combatLoadoutNode;}
		public AtbCharacterProperties AtbCharacterProperties => _atbCharacterProperties;
		public CharacterProperties CharacterProperties => _characterProperties;
		public Area3D HurtBoxArea => _hurtBoxArea;
		public bool LockOn { get => _lockOn; set => _lockOn = value; }
		#endregion

		public override void Init()
		{
			NodeChecking();
		}

		public void InitCombatActor(Identification id, IAtbCharacterService atbCharacterService)
		{
			Log.PrintMessage("InitCombatActor");
			_combatActor = new CombatActor(_atbCharacterProperties, _combatLoadout, id, atbCharacterService);
		}

	
		private void NodeChecking()
		{
			string noNode = "There is no ";
			DebugLamplighter.Assert(_combatLoadoutNode != null, $"{noNode} CombatLoadout is null");
			DebugLamplighter.Assert(_atbCharacterProperties != null, $"{noNode} AtbCharacterProperties is null");
			DebugLamplighter.Assert(_combatBrainNode != null, "There is no _combatBrainNode is null");
			DebugLamplighter.Assert(_hurtBoxAreaNode != null, "There is no HitBoxArea");

			if(_combatLoadoutNode != null)
				_combatLoadout  = GetNode<CombatLoadout>(_combatLoadoutNode);

			
			if(_combatBrainNode != null)
				_combatBrain = GetNode<ICombatBrain>(_combatBrainNode);

			if(_hurtBoxAreaNode != null)
				_hurtBoxArea = GetNode<Area3D>(_hurtBoxAreaNode);
		}

		public override void _ExitTree()
		{
			base._ExitTree();
		}
	}
}