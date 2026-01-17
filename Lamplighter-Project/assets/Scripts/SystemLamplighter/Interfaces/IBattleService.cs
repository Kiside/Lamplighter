using System;
using System.Collections.Generic;
using Characters.Interfaces;
using SystemLamplighter;

public interface IBattleService
{
	public void SetActors(IReadOnlyList<ICombatActor> actors);
	public void StartCombat(bool autoStartCombat);
	public void StartCombat();
}