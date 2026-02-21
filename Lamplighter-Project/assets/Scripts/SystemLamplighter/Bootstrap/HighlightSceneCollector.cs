using System.Collections.Generic;
using Godot;
using Microsoft.Extensions.DependencyInjection;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.Interfaces;

namespace SystemLamplighter.Bootstrap;

public partial class HighlightSceneCollector : BaseCollector
{
	private IHighlightSystem _highlightSystem;

	public override void _Ready()
	{
		base._Ready();
		Init();
	}

	public void Init()
	{
		_highlightSystem = GameBootstrap.Services
		.GetRequiredService<IHighlightSystem>();

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

		foreach(var a in array)
		{
			if(a is IHighlighter highlightable)
				highlightable.InitHighlighterSystem(_highlightSystem);
		}
	}
}