using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Characters.Interfaces;
using Godot;
using Microsoft.Extensions.DependencyInjection;
using SystemLamplighter;
using SystemLamplighter.Debug;

namespace SystemLamplighter
{
	public partial class CombatSceneCollector : Node
	{

		[Export]
		private Godot.Collections.Array<GroupsName> _groups;
		[Export]
		private bool _autoStartCombat = true;

		

		private IBattleService _battleService;
		private ICombatActorRegistry _combatActorRegistry;

		public override void _Ready()
		{
			base._Ready();

			Init();
			StartCombat();
		}

		private void Init()
		{

			_battleService = GameBootstrap.Services
			.GetRequiredService<IBattleService>();

			_combatActorRegistry = GameBootstrap.Services.
			GetRequiredService<ICombatActorRegistry>();
			
			GetCombatActorsHandler();
		}
		
		/// <summary>
		/// Metodo di base per avviare il combattimento
		/// </summary>
		private void StartCombat() => _battleService.StartCombat(_autoStartCombat);
		
		/// <summary>
		/// Metodo per prendere tutti gli actors in combattimento
		/// </summary>
		private void GetCombatActorsHandler()
		{
			DebugLamplighter.Assert(_groups != null, "_groups is null");

			if(_groups == null && _groups.Count <= 0)
				return;

			Godot.Collections.Array<Node> array = new Godot.Collections.Array<Node>();
			foreach(var g in _groups)
			{
				
				Log.PrintMessage($"searching for: {g}");				
				array.AddRange(GetTree().GetNodesInGroup($"{g}"));
			}

			InitActors(array);
		}
		
		/// <summary>
		/// Metodo per inizializzare la lista di actors in combattimento 
		/// </summary>
		/// <param name="array"></param>
		private void InitActors(Godot.Collections.Array<Node> array)
		{
			if(array.Count <= 0)
				return;

			List<ICombatActor> actors = new List<ICombatActor>();
			foreach(var a in array)
				{
					Log.PrintMessage($"- {a.Name}");
					if(a is IHasCombatInterface<ICombatActor> combat)
					{
						actors.Add(combat.GetCombatInterface());
					}
				}
			
			
			_combatActorRegistry.Init(actors);
		}
	}
}	