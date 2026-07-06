using System.Collections.Generic;
using Godot;
using Microsoft.Extensions.DependencyInjection;
using SystemLamplighter.Interfaces;
using SystemLamplighter.Target.Interfaces;

namespace SystemLamplighter.Bootstrap;

/// <summary>
/// Classe per la gestione dei oggetti targetable di tipo Node.
/// Lo scopo della classe è quello di generare e mantenere le istanze che serviranno
/// per la gestione dei oggetti targettabili
/// </summary>
public partial class TargetableSceneCollector : BaseCollector
{
	ITargetableProvider _targetableProvider;

	public override void _Ready()
	{
		Init();
		base._Ready();
	}

	public void Init()
	{
		_targetableProvider = GameBootstrap.Services.GetRequiredService<ITargetableProvider>();

		GetTargetablesNodes();
	}

	public void GetTargetablesNodes()
	{
		var array = GetNodesOfGroups();

		InitTargetablesProvider(array);
	}

	public void InitTargetablesProvider(Godot.Collections.Array<Node> nodes)
	{
		List<ITargetable> targetables = new List<ITargetable>();
		foreach(var n in nodes)
		{
			if(n is ITargetable targetable)
				targetables.Add(targetable);
		}

		_targetableProvider.Init(targetables);
	}
}