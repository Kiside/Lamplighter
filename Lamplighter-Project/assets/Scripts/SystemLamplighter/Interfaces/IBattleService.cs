using System;
using System.Collections.Generic;
using Characters.Interfaces;

public interface IBattleService
{
	public void StartCombat(IReadOnlyList<ICombatActor> actors, bool autoStartCombat);
	public void StartCombat(IReadOnlyList<ICombatActor> actors);
}