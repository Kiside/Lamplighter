using System;
using System.Collections.Generic;
using Characters.Interfaces;
using SystemLamplighter;

public interface IBattleService
{
	public void StartCombat(bool autoStartCombat);
	public void StartCombat();
}