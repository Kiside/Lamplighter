using System;
using Godot;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.DataStructure.EffectsData;
using SystemLamplighter.DataStructure.GeneralData;

namespace SystemLamplighter.DataStructure.ActionsData
{
	/// <summary>
	/// Classe per l'azione di un item
	/// </summary>
	[GlobalClass]
	public partial class ItemAction : EquipableActionData
	{
		[Export]
		protected string _description;
		public string Description => _description;

		public ItemAction() : 
		this(ActionType.ITEM, "", 1f, new TargetData(), null, new Godot.Collections.Array<EffectData>(), false ,"") { }

		public ItemAction(ActionType actionType, 
		string name, 
		float actionSpeedMultiplier, 
		TargetData targetData,
		Animation animation,
		Godot.Collections.Array<EffectData> effects,
		bool isEquipped,
		string description) 
		: base(actionType, name, actionSpeedMultiplier, targetData, animation, effects, isEquipped)
		{
			_description = description;
		}
	}
}