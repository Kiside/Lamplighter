using System.Collections.Generic;
using Characters.Interfaces;
using Godot;
using SystemLamplighter.Target.Interfaces;


namespace SystemLamplighter.Target;

/// <summary>
/// Classe i dati da passare per la risoluzione della fase di targetizzazione
/// </summary>
public class TargetResolutionContext
{
	public List<ITargetable> Targetables { get; private set; }
	public ICombatActor CasterActor { get; private set; }

	public TargetResolutionContext(List<ITargetable> targetables, ICombatActor casterActor)
	{
		Targetables = targetables;
		CasterActor = casterActor;
	}

	public TargetResolutionContext()
	{
		
	}
}