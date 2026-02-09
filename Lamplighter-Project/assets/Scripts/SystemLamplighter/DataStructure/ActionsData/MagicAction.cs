using System;
using System.Diagnostics;
using Godot;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.DataStructure.EffectsData;
using SystemLamplighter.DataStructure.GeneralData;

namespace SystemLamplighter.DataStructure.ActionsData
{
	/// <summary>
	/// Classe per l'azione magica
	/// </summary>
	[GlobalClass]
	public partial class MagicAction : EquipableActionData
	{
		[Export]
		// Costo del mana
		private int _manaCost;

		public int ManaCost => _manaCost;

		public MagicAction() : 
		this(ActionType.MAGIC, "", 1f, new TargetData(), null, new Godot.Collections.Array<EffectData>(), false, 0) { }

		public MagicAction(ActionType actionType, 
		string name, 
		float actionSpeedMultiplier, 
		TargetData targetData,
		Animation animation,
		Godot.Collections.Array<EffectData> effects,
		bool isEquipped
		,int manaCost)
		: base(actionType, name, actionSpeedMultiplier, targetData, animation, effects, isEquipped)
		{
			_actionType = actionType;
			_manaCost = manaCost;
			_isEquipped = isEquipped;
		}

	}
}