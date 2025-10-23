using Godot;
using System;
using System.Collections.Generic;

namespace SystemLamplighter
{
	public partial class ActionTimeBattleModel : AbstractModel
	{
		private List<AtbCharacter> _charactersInCombat;

		/// <summary>
		/// Aggiunge un personaggio alla lista
		/// </summary>
		/// <param name="character">personaggio</param>
		public void AddCharacter(AtbCharacter character)
		{
			_charactersInCombat.Add(character);
		}

		/// <summary>
		/// Pulisce l'intera lista
		/// </summary>
		public void ClearCharacters()
		{
			_charactersInCombat.Clear();
		}

		/// <summary>
		/// Rimuove un personaggio dalla lista
		/// </summary>
		/// <param name="character"></param>
		public void RemoveCharacter(AtbCharacter character)
		{
			if (!_charactersInCombat.Remove(character))
				Log.PrintWarning($"Non è stato possibile rimuovere {character.Name}");
		}
	}
}