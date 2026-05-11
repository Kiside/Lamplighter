using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Characters.Playable;
using Godot;
using SystemLamplighter.Interfaces;

public partial class HitBoxArea : Area3D
{
	public List<IEffectData> ActionData {get { return ActionData; } private set
		{
			ActionData = null;
			if(this.GetParent() is LamplighterCharacterController character)
				ActionData = character.CombatActor.CurrentAction.Effects.ToList();	
		}
	}
}