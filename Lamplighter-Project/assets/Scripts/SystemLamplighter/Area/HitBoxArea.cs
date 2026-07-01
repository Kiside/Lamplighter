using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Characters.Playable;
using Godot;
using SystemLamplighter.Interfaces;

namespace SystemLamplighter.Area;

// TODO: FORSE È DA RIVEDERE PER ATTACCHI O ALTRO CHE NON HANNO UN CHARACTER PER ORA FUNZIONANO SOLO MELEE

/// <summary>
/// Classe nodo per la gestione delle hitbox e ritornare gli effetti
/// </summary>
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