using Godot;
using System;
using System.Collections.Generic;

namespace SystemLamplighter
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
			Log.PrintMessage("Add character");
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

		public float GetPosition(int index)
		{
			return _charactersInCombat[index].Position;
		}
	}
}