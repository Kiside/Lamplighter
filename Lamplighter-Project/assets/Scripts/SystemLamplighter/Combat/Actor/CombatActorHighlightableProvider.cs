using System.Collections.Generic;
using Characters.Interfaces;
using SystemLamplighter.Interfaces;

public class CombatActorHighlightableProvider : ICombatActorHighlightableProvider
{
	Dictionary<ICombatActor, IHighlightable> _highlightables;
	public CombatActorHighlightableProvider()
	{
		_highlightables = new Dictionary<ICombatActor, IHighlightable>();
	}

	public void Init(Dictionary<ICombatActor, IHighlightable> highlightables)
	{
		if (_highlightables != null && _highlightables.Count > 0)
			_highlightables.Clear();

		_highlightables = highlightables;
	} 

	public IHighlightable GetHighlightable(ICombatActor actor) => _highlightables[actor];

	public int Count => _highlightables.Count;
}