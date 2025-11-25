using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using SystemLamplighter;
using SystemLamplighter.DataStructure;

namespace SystemLamplighter.BattleMenu
{
	public partial class BattleMenuModel : AbstractModel
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
		private Godot.Collections.Array<HealItemAction> _items = new();

		private System.Collections.Generic.Dictionary<string, ActionData> _actionsDictionary;

		public override void Init()
		{
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

		public List<string> Get(SubMenuType subMenuType)
		{
			return subMenuType switch
			{
				SubMenuType.ATTACK => GetNames(_attack),
				SubMenuType.MAGIC => GetNames(_magic),
				SubMenuType.ITEMS => GetNames(_items),
				_ => new List<string>()
			};
		}

		public ActionData GetAction(string id)
		{
			_actionsDictionary.TryGetValue(id, out var actionData);
			return actionData;
		}

		private List<string> GetNames<[MustBeVariant] T>(Godot.Collections.Array<T> list) where T : ActionData
		{
			var result = new List<string>(list.Count);
			foreach (var action in list)
				result.Add(action.Name);

			return result;
		}
	}
}