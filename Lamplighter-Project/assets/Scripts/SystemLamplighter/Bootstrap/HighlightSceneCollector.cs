using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Characters.Interfaces;
using Godot;
using Microsoft.Extensions.DependencyInjection;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.Interfaces;

namespace SystemLamplighter.Bootstrap;

public partial class HighlightSceneCollector : BaseCollector
{
	private IHighlightSystem _highlightSystem;
	private ICombatActorHighlightableProvider _highlightableProvider;

	public override void _Ready()
	{
		base._Ready();
		Init();
	}

	public void Init()
	{
		_highlightSystem = GameBootstrap.Services
		.GetRequiredService<IHighlightSystem>();

		_highlightableProvider = GameBootstrap.Services
		.GetRequiredService<ICombatActorHighlightableProvider>();

		GetHighlightHandler();
	}

	public void GetHighlightHandler()
	{
		Godot.Collections.Array<Node> array = GetNodesOfGroups();
		if(array != null)
			InitHighlighter(array);
	}

	public void InitHighlighter(Godot.Collections.Array<Node> array)
	{
		if(array.Count <= 0)
			return;

		Dictionary<ICombatActor, IHighlightable> highlightables = new Dictionary<ICombatActor, IHighlightable>();

		foreach(var a in array)
		{
			if(a is IHighlighter highlightable)
				highlightable.InitHighlighterSystem(_highlightSystem);

			if(a is ICombatActor c && a is IHighlightable h)
			{
				highlightables.Add(c, h);
			}
		}

		_highlightableProvider.Init(highlightables);
	}
}