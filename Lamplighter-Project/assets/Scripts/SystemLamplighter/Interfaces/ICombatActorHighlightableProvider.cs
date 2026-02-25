using System.Collections.Generic;
using SystemLamplighter.Interfaces;
using Characters.Interfaces;
public interface ICombatActorHighlightableProvider
{
	public void Init(Dictionary<ICombatActor, IHighlightable> highlightables);

	public IHighlightable GetHighlightable(ICombatActor actor);
}