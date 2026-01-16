using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Characters.Interfaces;
using Godot;
using Microsoft.Extensions.DependencyInjection;
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

		private IBattleService _battleService;

		private List<ICombatActor> _combatActors;

		public override void _Ready()
		{
			base._Ready();

			Init();
			StartCombat();
		}

		private void Init()
		{
			if(_combatActors == null)
				_combatActors = new List<ICombatActor>();

			
			_battleService = GameBootstrap.Services
			.GetRequiredService<IBattleService>();

			GetCombatActorsHandler();
		}

		/// <summary>
		/// Metodo richiamabile da fuori per avviare il combattimento
		/// </summary>
		public void TriggerStartCombat() => _battleService.StartCombat(_combatActors);

		/// <summary>
		/// Metodo di base per avviare il combattimento
		/// </summary>
		private void StartCombat() => _battleService.StartCombat(_combatActors, _autoStartCombat);
		
		/// <summary>
		/// Metodo per prendere tutti gli actors in combattimento
		/// </summary>
		private void GetCombatActorsHandler()
		{
			DebugLamplighter.Assert(_groups != null, "_groups is null");

			if(_groups == null && _groups.Count <= 0)
				return;
			
			foreach(var g in _groups)
			{
				Log.PrintMessage($"searching for: {g}");
				InitActors(GetTree().GetNodesInGroup($"{g}"));
			}
		}
		
		/// <summary>
		/// Metodo per inizializzare la lista di actors in combattimento 
		/// </summary>
		/// <param name="array"></param>
		private void InitActors(Godot.Collections.Array<Node> array)
		{
			foreach(var a in array)
				{
					Log.PrintMessage($"- {a.Name}");
					if(a is ICombatActor combatActor)
					{
						_combatActors.Add(combatActor);
					}
				}
		}
	}
}	