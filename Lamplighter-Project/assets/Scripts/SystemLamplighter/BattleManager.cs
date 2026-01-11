using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Characters.Interfaces;
using Godot;
using SystemLamplighter;
using SystemLamplighter.Debug;

namespace SystemLamplighter
{
	public partial class BattleManager : Node
	{

		[Export]
		private Godot.Collections.Array<GroupsName> _groups;
		[Export]
		private bool _autoStartCombat = true;

		private List<ICombatActor> _combatActors;

		private static BattleManager _instance;

		public static Action<List<ICombatActor>> TriggerOnStartCombat;

		public override void _Ready()
		{
			base._Ready();

			Init();
			StartBattleHandler();
		}

		private void Init()
		{
			if(_instance != null || _instance != this)
				_instance.QueueFree();
			
			_instance = this;

			if(_combatActors == null)
				_combatActors = new List<ICombatActor>();

			GetCombatActorsHandler();
		}

		public static void StartBattle() => _instance?.StartBattleHandler();

		private void StartBattleHandler()
		{
			if(_autoStartCombat)
				TriggerOnStartCombat?.Invoke(_combatActors);

		}

		private void GetCombatActorsHandler()
		{
			DebugLamplighter.Assert(_groups != null, "_groups is null");

			if(_groups == null && _groups.Count <= 0)
				return;
			
			foreach(var g in _groups)
			{
				InitActors(GetTree().GetNodesInGroup($"{g}"));
			}
		}
		
		private void InitActors(Godot.Collections.Array<Node> array)
		{
			foreach(var a in array)
				{
					if(a is ICombatActor combatActor)
					{
						_combatActors.Add(combatActor);
					}
				}
		}

		private static void StaticGetCombatActors() => _instance?.GetCombatActorsHandler();
	}
}