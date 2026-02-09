using System.Collections.Generic;
using Godot;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.DataStructure.EffectsData;

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
	public ITargetData TargetData {get;}
	public IEnumerable<IEffectData> Effects {get;}
}