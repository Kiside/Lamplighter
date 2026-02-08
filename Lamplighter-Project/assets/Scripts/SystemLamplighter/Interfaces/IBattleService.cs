using System;
using System.Collections.Generic;
using Characters.Interfaces;
using SystemLamplighter;

namespace SystemLamplighter.Interfaces;

/// <summary>
/// Interfaccia IBattleService, servizio per il combattimento
/// </summary>
public interface IBattleService
{
	public void StartCombat(bool autoStartCombat);
	public void StartCombat();
	public void StopAtb();
}