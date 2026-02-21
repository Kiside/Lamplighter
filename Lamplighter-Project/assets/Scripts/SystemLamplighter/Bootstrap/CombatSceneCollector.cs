using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Characters.Interfaces;
using Godot;
using Microsoft.Extensions.DependencyInjection;
using SystemLamplighter;
using SystemLamplighter.Debug;
using SystemLamplighter.Interfaces;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.Tool;

namespace SystemLamplighter.Bootstrap
{
	/// <summary>
	/// Classe per la gestione del combattimento di tipo Node.
	/// Lo scopo della classe è quello di generare e mantenere le istanze che serviranno
	/// per l'inizio e la gestione del combattimento
	/// </summary>
	public partial class CombatSceneCollector : BaseCollector
	{
		[Export]
		private bool _autoStartCombat = true;

		

		private IBattleService _battleService;
		private ICombatActorRegistry _combatActorRegistry;
		private ICombatActorPositionProvider<Node3D> _combatActorPositionProvider;

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

			_combatActorPositionProvider = GameBootstrap.Services.
			GetRequiredService<ICombatActorPositionProvider<Node3D>>();
			
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
			Godot.Collections.Array<Node> array = GetNodesOfGroups();
			if(array != null)
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
			Dictionary<ICombatActor, Node3D> positions = new Dictionary<ICombatActor, Node3D>();
			foreach(var a in array)
				{
					Log.PrintMessage($"- {a.Name}");
					if(a is IHasCombatInterface<ICombatActor> combat)
					{
						actors.Add(combat.GetCombatInterface());
						if(a is Node3D node3D)
							positions.Add(combat.GetCombatInterface(), node3D);
					}
				}
			
			
			_combatActorRegistry.Init(actors);
			_combatActorPositionProvider.Init(positions);
		}
	}
}	