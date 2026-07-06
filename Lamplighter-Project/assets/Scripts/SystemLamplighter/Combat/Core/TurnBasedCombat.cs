using SystemLamplighter.BattleMenu;
using Characters.Interfaces;
using MessagePipe;
using SystemLamplighter.Events;
using System.Linq;
using SystemLamplighter.DataStructure.ActionsData;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.Tool;
using SystemLamplighter.ATB;
using SystemLamplighter.Interfaces;
using Characters.Loadout;
using System.Runtime.InteropServices;
using System;
using SystemLamplighter.DataStructure.GeneralData;
using SystemLamplighter.Debug;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using SystemLamplighter.Target;
using SystemLamplighter.Navigation;


namespace SystemLamplighter.Combat.Core
{
	/// <summary>
	/// Classe che si occupa di gestire il combattimento del giocatore
	/// </summary>
	public class TurnBasedCombat : ITurnBasedCombat, ICombatCommandHandler, ICombatActionExecutor
	{
		#region PUBLIC VARIABLES
		public BattleMenuController _battleMenuController {get; private set;}
		// TODO: Controllare se Actor serve nel contesto, dato che viene passato più volte anche da altre parti
		public ICombatActor Actor {get; private set;}
		public CombatLoadout CombatLoadout {get => Actor.CombatLoadout;}
		public AtbCharacterProperties AtbProperties {get => Actor.AtbProperties;}
		public IActionData CurrentAction { get => Actor.CurrentAction; set => Actor.CurrentAction = value;}
		public TargetResolutionContext TargetResolutionContext {get; private set;}
		#endregion

		#region PRIVATE VARIABLES
		private MovementService _movementService;
		private readonly ITargetableProvider _targetableProvider;
		#endregion

		#region eventsAttributes
		private IPublisher<AtbCommandPhaseEndEvent> _publishCommandPhaseEnd;
		private IPublisher<AtbEndExecuteActionEvent> _publishEndExecuteAction;
		private IPublisher<StartTargetEvent> _publisherStartTarget;
		private ISubscriber<AtbCommandPhaseStartedEvent> _subscriberCommandPhaseStarted;
		private ISubscriber<AtbExecuteActionEvent> _subscriberExecuteActionEvent;
		private ISubscriber<EndTargetEvent> _subscriberEndTarget;
		private IDisposable _disposeEndTargetEvent;
		private readonly DisposableBagBuilder _bag;
		#endregion

		AnimationPlayer _animationPlayer;

		public TurnBasedCombat(ITargetableProvider targetableProvider)
		{
			//_movementResolver = movementResolver;
			_targetableProvider = targetableProvider;

			_bag = DisposableBag.CreateBuilder();
		}

		public void Init(TurnBasedCombatContext turnBasedCombatContext, IMovementService movementService, AnimationPlayer animationPlayer)
		{
			_movementService = movementService as MovementService;

			if(turnBasedCombatContext.CombatBrain is BattleMenuController battleMenuController)
				_battleMenuController = battleMenuController;
			else
				DebugLamplighter.Assert(true, "Il ICombatBrain non è un battleMenuController, quando dovrebbe");

			Actor = turnBasedCombatContext.Actor;
			_publishCommandPhaseEnd = turnBasedCombatContext.PublishCommandPhaseEnd;
			_publishEndExecuteAction = turnBasedCombatContext.PublishEndExecuteAction;
			_publisherStartTarget = turnBasedCombatContext.PublisherStartTarget;
			_subscriberCommandPhaseStarted = turnBasedCombatContext.SubscriberCommandPhaseStarted;
			_subscriberExecuteActionEvent = turnBasedCombatContext.SubscriberExecuteAction;
			_subscriberEndTarget = turnBasedCombatContext.SubscriberEndTarget;

			_subscriberCommandPhaseStarted.Subscribe(OnCommandPhaseStarted).AddTo(_bag);
			_subscriberExecuteActionEvent.Subscribe(OnExecuteCombatAction).AddTo(_bag);
			
			_battleMenuController.OnActionClick += ActionChoosedHandler;
			_battleMenuController.OnOpenSubMenu += OpenBattleSubMenuHandler;

			_animationPlayer = animationPlayer;
		}

		#region EVENTS HANDLER

		// todo: AGGIUNGERE ANCHE QUESTO ALL'INTERFACCIA?
		/// <summary>
		/// Metodo chiamato quando verrà eseguita l'azione
		/// </summary>
		/// <param name="ev"></param>
		public void OnExecuteCombatAction(AtbExecuteActionEvent ev)
		{
			if (ev.Actor != Actor)
				return;

			
			// Bisogna capire la distanza dal target
			var casterPos = _targetableProvider.GetTargetable(ev.Actor.Id).Position;
			float maxDistanceFromTarget = 0f;
			var targetables = TargetResolutionContext.Targetables;


			foreach(var target in targetables)
			{
				var distanceFromTarget = casterPos.DistanceSquaredTo(target.Position);
				maxDistanceFromTarget = distanceFromTarget > maxDistanceFromTarget ? distanceFromTarget : maxDistanceFromTarget;
			}
			// Confrontare la distanza dal target con il range dell'attacco
			if(CurrentAction.TargetData.Range != 0f && maxDistanceFromTarget > CurrentAction.TargetData.Range)
			{
				_movementService.SetCalculatedOptmizeTargetPosition(casterPos, targetables[0].Position, CurrentAction.TargetData.Range);
			}
			else
			{
				// In altri casi non bisogna muoversi
				_movementService.SetTargetPosition(targetables[0].Position);
			}

			// Ad azione eseguita resetto la posizione del personaggio sull'ATB
			//_publishEndExecuteAction.Publish(new AtbEndExecuteActionEvent(Actor));
			_ = ExecuteCombatActionTask();
		}

		
		// TODO DA AGGIUNGERE ALL'INTERFACCIA?
		public void OnCommandPhaseStarted(AtbCommandPhaseStartedEvent evt)
		{
			if (evt.Actor != Actor)
				return;

			// TODO: forse non deve essere qui che si gestisce tale evento
			_battleMenuController.TurnOn();
		}

		// TODO DA AGGIUNGERE ALL'INTERFACCIA?
		public void OnEndTarget(EndTargetEvent ev)
		{
			DebugLamplighter.Assert(ev != null, "ev is null");
			DebugLamplighter.Assert(ev.TargetResolutionContext != null, "TargetResolutionContext is null");

			if (ev.TargetResolutionContext.CasterActor != Actor)
				return;

			TargetResolutionContext = null;
			_disposeEndTargetEvent?.Dispose();
			// TODO: la riga EndCommandStatus(...) può essere gestita dall'evento _publishCommandPhaseEnd? Controllare e provare
			//AtbProperties.EndCommandStatus(CurrentAction.ActionSpeedMultiplier);
			TargetResolutionContext = ev.TargetResolutionContext;
			_publishCommandPhaseEnd.Publish(new AtbCommandPhaseEndEvent(Actor));
		}
		#endregion

		/// <summary>
		/// Task Asincrono che si occupa di aspettare il momento giusto per poter eseguire l'azione
		/// </summary>
		/// <returns></returns>
		private async Task ExecuteCombatActionTask()
		{
			try
			{
				while(!_movementService.IsNavigationFinished())
				{
					await Task.Delay(100);
				}
				
				_animationPlayer.Play(Actor.CurrentAction.Animation.ResourceName);

				while(_animationPlayer.IsPlaying())
				{
					await Task.Delay(100);
				}
				_animationPlayer.Stop();
				
				// TODO: il personaggio deve trovare una posizione in cui andare 
				_publishEndExecuteAction.Publish(new AtbEndExecuteActionEvent(Actor));
			}
			catch (Exception ex)
			{
				Log.PrintWarning($"{ex}");
			}
			
		}


		/// <summary>
		/// Quando l'utente sceglie nel primo menu cosa fare (attaccare, usare un oggetto, scappare o ecc...)
		/// </summary>
		/// <param name="subMenuIds"></param>
		public void OpenBattleSubMenuHandler(ISubMenuDefinition subMenuIds)
		{
			if (subMenuIds.IsImmediate)
			{
				CurrentAction = subMenuIds.BuildAction(Actor).First();
				_publishCommandPhaseEnd.Publish(new AtbCommandPhaseEndEvent(Actor));
				return;
			}

			var actions = subMenuIds.BuildAction(Actor);
			_battleMenuController.OpenSubMenu(actions);

		}

		public void ActionChoosedHandler(IActionData action)
		{
			CurrentAction = action;

			_disposeEndTargetEvent =_subscriberEndTarget.Subscribe(OnEndTarget);
			_publisherStartTarget.Publish(new StartTargetEvent(Actor, CurrentAction));
		}

		public void Dispose()
		{
			_bag.Build().Dispose();

			_disposeEndTargetEvent?.Dispose();

			_battleMenuController.OnActionClick -= ActionChoosedHandler;
			_battleMenuController.OnOpenSubMenu -= OpenBattleSubMenuHandler;
		}
	}
}
