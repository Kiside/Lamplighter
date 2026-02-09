using System;
using System.Diagnostics;
using Godot;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.DataStructure.EffectsData;
using SystemLamplighter.DataStructure.GeneralData;

namespace SystemLamplighter.DataStructure.ActionsData
{
	/// <summary>
	/// Questa classe serve per specificare se una action è una action da equipaggiare o no
	/// </summary>
	[GlobalClass]
	public partial class EquipableActionData : ActionData
	{
		[Export]
		protected bool _isEquipped;

		public bool IsEquipped;

		public EquipableActionData() : 
		this(ActionType.ATTACK, "", 1f, new TargetData(), null, new Godot.Collections.Array<EffectData>(), false) { }

		public EquipableActionData(ActionType actionType, 
		string name, 
		float actionSpeedMultiplier, 
		TargetData targetData,
		Animation animation,
		Godot.Collections.Array<EffectData> effects,
		bool isEquipped)
		: base(actionType, name, actionSpeedMultiplier, targetData, animation, effects)
		{
			_isEquipped = isEquipped;
		}
	}
}