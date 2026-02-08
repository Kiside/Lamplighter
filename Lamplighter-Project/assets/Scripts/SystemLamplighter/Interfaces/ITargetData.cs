using SystemLamplighter.Common.Enums;

namespace SystemLamplighter.Interfaces;

/// <summary>
/// Interfaccia per i dati dei target
/// </summary>
public interface ITargetData
{
	public TargetType TargetType {get;}
	public float Diameter {get;}
	public int NumberOfTargets {get;}
	public float Range{get;}

	
}