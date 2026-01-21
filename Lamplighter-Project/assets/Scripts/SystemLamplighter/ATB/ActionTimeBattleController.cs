using Characters.Interfaces;
using Godot;
using MessagePipe;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using SystemLamplighter.Debug;
using SystemLamplighter.Events;
using SystemLamplighter.Interfaces;
using SystemLamplighter.Extensions;
using System.Linq;
using SystemLamplighter.ATB.Interfaces;
using Microsoft.Extensions.DependencyInjection;


namespace SystemLamplighter.ATB
{
	public partial class ActionTimeBattleController() : AbstractController<ActionTimeBattleView, ActionTimeBattleModel>
	{
		bool _inCharging = false;
		int _currentIndex;
		IAtbService _atbService;

		public event Action OnCommandEvent;

		//private readonly DisposableBagBuilder _bag = DisposableBag.CreateBuilder();
		#nullable enable
		private IDisposable? _atbCommandEndSubscription;
		private IDisposable? _subscriptionCombatStarted;
		#nullable disable
		private ICombatActor _currentActorInCommand;

		// PER DEBUG
		public void CallViewUpdatePosition(float position, int index) => _view.UpdatePosition(position, index);

		public override void Init()
		{
			base.Init();

			Subscribe();

			_atbService = new AtbService(
			false, 
			GameBootstrap.Services.GetRequiredService<ICombatActorRegistry>(),
			this.GetSubscriber<AtbCommandPhaseEndEvent>(),
			this.GetSubscriber<CombatEndEvent>(),
			this.GetPublisher<AtbCommandPhaseStartedEvent>(),
			this.GetPublisher<AtbExecuteActionEvent>()
			);
		}


		private void StartCombat(CombatStartedEvent ev)
		{		
			AddCharacters(ev.Actors.ToList());
			_atbService.ActivateATB(true);
		}

		public override void _PhysicsProcess(double delta)
		{
			if(_atbService != null && _atbService.ClockingAtb(delta))
				_view.UpdatePositions();
			//ClockingAtb(delta);
		}

		
		/// <summary>
		/// Aggiunge un personaggio alla lista
		/// </summary>
		/// <param name="character">personaggio</param>
		public void AddCharacter(ICombatActor character)
		{
			DebugLamplighter.Assert(character != null, "character is null");
			Assert();

			character.AtbProperties.Subscribe();
			//_model.AddCharacter(character);
			_view.AddCharacter(character);
		}

		/// <summary>
		/// Aggiunge una lista di personaggi
		/// </summary>
		/// <param name="characters"></param>
		public void AddCharacters(List<ICombatActor> characters)
		{
			DebugLamplighter.Assert(characters != null, "character is null");
			DebugLamplighter.Assert(characters.Count > 0, "character count is zero");

			foreach(var c in characters)
			{
				AddCharacter(c);
			}
		}

		public override void _ExitTree()
		{
			base._ExitTree();

			_atbService.Dispose();
			Unsubscribe();
		}

		private void Subscribe()
		{
			_subscriptionCombatStarted = this.SubscribeEvent<CombatStartedEvent>(StartCombat);
		}

		private void Unsubscribe()
		{
			_subscriptionCombatStarted.Dispose();
		}
	}
}
