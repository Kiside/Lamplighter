using Characters.Interfaces;
using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using SystemLamplighter.Debug;

namespace SystemLamplighter.ATB
{
	public partial class ActionTimeBattleModel : AbstractModel
	{
		private List<ICombatActor> _charactersInCombat;

		public int CharactersCount => _charactersInCombat.Count;
		public List<ICombatActor> Characters => _charactersInCombat;

		public override void Init()
		{
			if(_charactersInCombat == null)
				_charactersInCombat = new List<ICombatActor>();
		}


		/// <summary>
		/// Aggiunge un personaggio alla lista
		/// </summary>
		/// <param name="character">personaggio</param>
		public void AddCharacter(ICombatActor character)
		{
			DebugLamplighter.Assert(character != null, "character is null");
			DebugLamplighter.Assert(_charactersInCombat != null, "_charactersInCombat is null");

			_charactersInCombat.Add(character);
		}

		/// <summary>
		/// Pulisce l'intera lista
		/// </summary>
		public void ClearCharacters()
		{
			DebugLamplighter.Assert(_charactersInCombat != null, "_charactersInCombat is null");

			foreach(var c in _charactersInCombat)
			{
				c.AtbProperties.Unsubscribe();
			}
			_charactersInCombat.Clear();
		}

		/// <summary>
		/// Rimuove un personaggio dalla lista
		/// </summary>
		/// <param name="character"></param>
		public void RemoveCharacter(ICombatActor character)
		{
			DebugLamplighter.Assert(_charactersInCombat != null, "_charactersInCombat is null");

			if (!_charactersInCombat.Remove(character))
				Log.PrintWarning($"Non è stato possibile rimuovere {character.AtbProperties.Name}");
		}

		public float GetPosition(int index)
		{
			DebugLamplighter.Assert(_charactersInCombat != null, "_charactersInCombat is null");
			DebugLamplighter.Assert(index > -1, "index is negative");
			DebugLamplighter.Assert(index < _charactersInCombat.Count, "index goes overflow");

			return _charactersInCombat[index].AtbProperties.Position;
		}
	}
}