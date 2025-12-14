using System;
using Godot;
using SystemLamplighter.DataStructure;
using System.Diagnostics;
using System.Collections.Generic;

namespace Characters
{
	/// <summary>
	/// Insieme di dati per il combattimento, dentro il combat menu
	/// </summary>
	public partial class CombatLoadout : Node
	{
		[ExportGroup("Attacks")]
		[Export]
		private Godot.Collections.Array<AttackAction> _attack = new();

		[ExportGroup("Guard")]
		[Export]
		private Godot.Collections.Array<DefenseAction> _defense = new();

		[ExportGroup("Magic")]
		[Export]
		private Godot.Collections.Array<MagicAction> _magic = new();

		[ExportGroup("Items")]
		[Export]
		private Godot.Collections.Array<ItemAction> _items = new();

		private System.Collections.Generic.Dictionary<string, ActionData> _actionsDictionary;

		public void Init()
		{
			Debug.Assert(_attack != null, "_attack is null");
			Debug.Assert(_magic != null, "_magic is null");
			Debug.Assert(_items != null, "_items is null");
			Debug.Assert(_defense != null, "_defense is null");

			if (_actionsDictionary is null)
				_actionsDictionary = new System.Collections.Generic.Dictionary<string, ActionData>();
				
			SetupDictionary();
		}

		private void SetupDictionary()
		{
			if (_actionsDictionary is not null)
				_actionsDictionary.Clear();

			foreach (var attack in _attack)
			{
				_actionsDictionary.Add(attack.Name, attack);
			}

			foreach (var defense in _defense)
			{
				_actionsDictionary.Add(defense.Name, defense);
			}
			foreach (var magic in _magic)
			{
				_actionsDictionary.Add(magic.Name, magic);
			}

			foreach (var item in _items)
			{
				_actionsDictionary.Add(item.Name, item);
			}
		}

		public List<string> GetAttacksId()
		{
			Debug.Assert(_attack != null, "_attack is null");
			Debug.Assert(_attack.Count > 0, "_attack is empty");

			return PrepareList(_attack);
		}
		public List<string> GetMagicsId()
		{
			Debug.Assert(_magic != null, "_magic is null");
			Debug.Assert(_magic.Count > 0, "_magic is empty");

			return PrepareList(_magic);
		}
		public List<string> GetItemsId()
		{
			Debug.Assert(_items != null, "_attack is null");
			Debug.Assert(_items.Count > 0, "_attack is empty");

			return PrepareList(_items);
		}

		private List<string> PrepareList<[MustBeVariant] T>(Godot.Collections.Array<T> list) where T : ActionData
		{
			var result = new List<string> ();
			foreach(var value in list)
			{
				if (value is EquipableActionData equip && equip.IsEquipped)
				{
					result.Add(value.Name);
				}
				else
				{
					result.Add(value.Name);
				}
				
			}
			return result;
		}
	}
}