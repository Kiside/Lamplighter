using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Characters.Playable;
using Godot;
using SystemLamplighter.Interfaces;

public partial class HitBoxArea : Area3D
{
	private List<IEffectData> _actionData;

	public List<IEffectData> ActionData 
	{ 
		get 
		{ 
			_actionData = null;
			if(this.GetParent() is LamplighterCharacterController character)
				_actionData = character.CombatActor.CurrentAction.Effects.ToList();

			return _actionData;   
		} 
	}
}