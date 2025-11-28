using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SystemLamplighter.ATB
{
	public partial class ActionTimeBattleModel : AbstractModel
	{
		private List<AtbCharacter> _charactersInCombat;

		public int CharactersCount => _charactersInCombat.Count;
		public List<AtbCharacter> Characters => _charactersInCombat;

		public override void Init()
		{
			_charactersInCombat = new List<AtbCharacter>();
		}


		/// <summary>
		/// Aggiunge un personaggio alla lista
		/// </summary>
		/// <param name="character">personaggio</param>
		public void AddCharacter(AtbCharacter character)
		{
			Debug.Assert(character != null, "character is null");
			Debug.Assert(_charactersInCombat != null, "_charactersInCombat is null");

			_charactersInCombat.Add(character);
		}

		/// <summary>
		/// Pulisce l'intera lista
		/// </summary>
		public void ClearCharacters()
		{
			Debug.Assert(_charactersInCombat != null, "_charactersInCombat is null");

			_charactersInCombat.Clear();
		}

		/// <summary>
		/// Rimuove un personaggio dalla lista
		/// </summary>
		/// <param name="character"></param>
		public void RemoveCharacter(AtbCharacter character)
		{
			Debug.Assert(_charactersInCombat != null, "_charactersInCombat is null");

			if (!_charactersInCombat.Remove(character))
				Log.PrintWarning($"Non è stato possibile rimuovere {character.Name}");
		}

		public float GetPosition(int index)
		{
			Debug.Assert(_charactersInCombat != null, "_charactersInCombat is null");
			Debug.Assert(index > -1, "index is negative");
			Debug.Assert(index < _charactersInCombat.Count, "index goes overflow");

			return _charactersInCombat[index].Position;
		}
	}
}