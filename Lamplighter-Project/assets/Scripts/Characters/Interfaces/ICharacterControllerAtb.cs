using System;
using SystemLamplighter;

/// <summary>
/// Intefaccia per i personaggi che hanno l'ATB
/// Ogni metodo di questa interfaccia deve avere l'inizio del nome che inizia con "ATB_..."
/// </summary>
public interface ICharacterControllerAtb
{
	public void ATB_Action();
	public AtbCharacterProperties ATB_GetCharacterProperties();
}