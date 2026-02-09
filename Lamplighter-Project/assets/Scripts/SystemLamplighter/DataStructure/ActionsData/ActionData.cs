using System;
using System.Diagnostics;
using Godot;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.DataStructure.GeneralData;
using SystemLamplighter.Interfaces;
using SystemLamplighter.DataStructure.EffectsData;
using System.Collections.Generic;
using System.Linq;

namespace SystemLamplighter.DataStructure.ActionsData
{
	[GlobalClass]
	/// <summary>
	/// Classe generica per le azioni
	/// </summary>
	public partial class ActionData : Resource, IActionData
	{
		
		// Che tipo di azione è
		[Export]
		protected ActionType _actionType;
		// Il nome dell'azione
		[Export]
		protected string _name;
		// La velocità che avrà l'azione
		[Export]
		protected float _actionSpeedMultiplier;
		// Dati del target
		[Export] 
		protected TargetData _targetData;
		// Animazione azione
		[Export]
		protected Animation _animation;
		// Effetti dell'azione
		[Export]
		protected Godot.Collections.Array<EffectData> _effects;

		public ActionType ActionType => _actionType;
		public string Name => _name;
		public float ActionSpeedMultiplier => _actionSpeedMultiplier;
		public ITargetData TargetData => _targetData;
		public Animation Animation => _animation;
		public IEnumerable<IEffectData> Effects => _effects;

		public ActionData() : 
		this(ActionType.ATTACK, "", 1f, new TargetData(), null, new Godot.Collections.Array<EffectData>() ) { }

		public ActionData(ActionType actionType, 
		string name, 
		float actionSpeedMultiplier, 
		TargetData targetData,
		Animation animation,
		Godot.Collections.Array<EffectData> effects)
		{
			_actionType = actionType;
			_name = name;
			_actionSpeedMultiplier = actionSpeedMultiplier;
			_targetData = targetData;
			_animation = animation;
			_effects = effects;
		}
	}
}