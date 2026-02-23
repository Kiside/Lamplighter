using Characters.Interfaces;
using Godot;
using MessagePipe;
using Microsoft.Extensions.DependencyInjection;
using SystemLamplighter.Bootstrap;
using SystemLamplighter.DataStructure.GeneralData;
using SystemLamplighter.Interfaces;
using SystemLamplighter.Target.Interfaces;

namespace SystemLamplighter.Target;

/// <summary>
/// Servizio per la targetizzazione, contiene la logica per risolvere i target
/// </summary>
public class TargetService
{
	private Camera3D _camera;

	private readonly ICombatActorRegistry _combatActors;
	private readonly ICombatActorPositionProvider<Node3D> _combatActorPosition;

	private ISubscriber<StartTargetEvent> _subscriberStartTarget;
	private IPublisher<EndTargetEvent> _publisherEndTarget;

	private readonly DisposableBagBuilder _bag;

	public TargetService(Camera3D camera,
	ISubscriber<StartTargetEvent> subscriberStartTarget, 
	IPublisher<EndTargetEvent> publisherEndTarget)
	{
		_bag = DisposableBag.CreateBuilder();

		_camera = camera;

		_combatActors = GameBootstrap.Services.GetRequiredService<ICombatActorRegistry>();
		_combatActorPosition = GameBootstrap.Services.GetRequiredService<ICombatActorPositionProvider<Node3D>>();

		_subscriberStartTarget = subscriberStartTarget;
		_publisherEndTarget = publisherEndTarget;
		
		_subscriberStartTarget.Subscribe(OnStartTarget).AddTo(_bag);


	}

	private void OnStartTarget(StartTargetEvent ev)
	{
		ResolveTargets(ev.Action, ev.Actor);
	}

	public void ResolveTargets(IActionData action, ICombatActor mainActor)
	{
		var targetData = action.TargetData;

		switch(targetData.TargetType)
		{
			case Common.Enums.TargetType.CIRCLE:
			break;
			case Common.Enums.TargetType.SELF:
			break;
			case Common.Enums.TargetType.SINGLE:
			HandleSingleTarget(targetData);
			break;
			case Common.Enums.TargetType.LINE:
			break;
			case Common.Enums.TargetType.CONE:
			break;
			case Common.Enums.TargetType.GROUP:
			break;
		}
	}

	private void HandleSingleTarget(ITargetData targetData)
	{
		_camera.LookAt(_combatActorPosition.GetPosition(_combatActors.GetActor(0)));


	}

	public void Dispose()
	{
		_bag.Build().Dispose();
	}

	
}