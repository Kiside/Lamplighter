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
		private ICombatActorProvider _combatProvider;

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

			_combatProvider = GameBootstrap.Services.
			GetRequiredService<ICombatActorProvider>();
			
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
			var array = GetNodesOfGroups();
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

			foreach(var a in array)
				{
					Log.PrintMessage($"- {a.Name}");
					if(a is ICombatActor combat)
					{
						actors.Add(combat);
					}
				}
			
			
			_combatProvider.Init(actors);
		}
	}
}	