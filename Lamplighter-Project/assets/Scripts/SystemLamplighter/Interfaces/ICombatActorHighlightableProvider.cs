using System.Collections.Generic;
using SystemLamplighter.Interfaces;
using Characters.Interfaces;

namespace SystemLamplighter.Interfaces;

// TODO: è obsoleto? 03/07/2026
public interface ICombatActorHighlightableProvider
{
	public void Init(Dictionary<ICombatActor, IHighlightable> highlightables);

	public IHighlightable GetHighlightable(ICombatActor actor);

	public int Count {get;}
}