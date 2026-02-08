using Godot;
using SystemLamplighter.Common.Enums;

namespace SystemLamplighter.Interfaces;

/// <summary>
/// Interfaccia IActionData
/// </summary>
public interface IActionData
{
	public string Name {get;}
	public float ActionSpeedMultiplier {get;}
	public ActionType ActionType {get;}
	public Animation Animation {get;}
}