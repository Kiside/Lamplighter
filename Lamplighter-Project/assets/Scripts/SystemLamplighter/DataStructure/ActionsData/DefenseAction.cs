using System;
using System.Diagnostics;
using Godot;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.DataStructure.EffectsData;
using SystemLamplighter.DataStructure.GeneralData;

namespace SystemLamplighter.DataStructure.ActionsData
{
	/// <summary>
	/// Classe per l'azione difensiva
	/// </summary>
	[GlobalClass]
	public partial class DefenseAction : ActionData
	{
		[Export]
		private float _defensiveAmount;

		public float DefensiveAmount => _defensiveAmount;

		public DefenseAction() : 
		this(ActionType.GUARD, "", 1f, new TargetData(), null, new Godot.Collections.Array<EffectData>(), 0f) { }

		public DefenseAction(ActionType actionType, 
		string name, 
		float actionSpeedMultiplier, 
		TargetData targetData,
		Animation animation,
		Godot.Collections.Array<EffectData> effects,
		float defensiveAmount) 
		: base(ActionType.GUARD, "", 1f, new TargetData(), null, new Godot.Collections.Array<EffectData>())
		{
			_defensiveAmount = defensiveAmount;
		}
	}
}