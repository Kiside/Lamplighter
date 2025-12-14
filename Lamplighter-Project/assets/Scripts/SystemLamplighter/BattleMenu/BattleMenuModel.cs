using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SystemLamplighter;
using SystemLamplighter.DataStructure;

namespace SystemLamplighter.BattleMenu
{
	public partial class BattleMenuModel : AbstractModel
	{
		// [ExportGroup("Attacks")]
		// [Export]
		// private Godot.Collections.Array<AttackAction> _attack = new();

		// [ExportGroup("Guard")]
		// [Export]
		// private Godot.Collections.Array<DefenseAction> _defense = new();

		// [ExportGroup("Magic")]
		// [Export]
		// private Godot.Collections.Array<MagicAction> _magic = new();

		// [ExportGroup("Items")]
		// [Export]
		// private Godot.Collections.Array<HealItemAction> _items = new();

		// private System.Collections.Generic.Dictionary<string, ActionData> _actionsDictionary;


		private List<string> _attacks;

		private List<string> _magics;
		private List<string> _items;

		public override void Init()
		{

		}

		public void Init(List<string> attacks, List<string> magics, List<string> items)
		{
			Debug.Assert(attacks != null, "can't initialize _attacks because the list is null");
			Debug.Assert(magics != null, "can't initialize _magics because the list is null");
			Debug.Assert(items != null, "can't initialize _items because the list is null");
			Debug.Assert(attacks.Count > 0, "can't initialize _attacks because the list is less then zero");
			Debug.Assert(magics.Count > 0, "can't initialize _magics because the list is less then zero");
			Debug.Assert(items.Count > 0, "can't initialize _items because the list is less then zero");

			_attacks = attacks;
			_magics = magics;
			_items = items;
		}

		
		public List<string> Get(SubMenuType subMenuType)
		{


			// Debug.Assert(_attack != null, "_attack is null");
			// Debug.Assert(_magic != null, "_magic is null");
			// Debug.Assert(_items != null, "_items is null");

			// return subMenuType switch
			// {
			// 	SubMenuType.ATTACK => GetNames(_attack),
			// 	SubMenuType.MAGIC => GetNames(_magic),
			// 	SubMenuType.ITEMS => GetNames(_items),
			// 	_ => new List<string>()
			// };

			return new List<string>();
		}

		// public ActionData GetAction(string id)
		// {
		// 	// Debug.Assert(id != null || id != String.Empty, "id is null or empty");
		// 	// Debug.Assert(_actionsDictionary != null, "_actionsDictionary is null");

		// 	// _actionsDictionary.TryGetValue(id, out var actionData);
		// 	// return actionData;
		// }

		private List<string> GetNames<[MustBeVariant] T>(Godot.Collections.Array<T> list) where T : ActionData
		{
			Debug.Assert(list != null, "list is null");

			var result = new List<string>(list.Count);
			foreach (var action in list)
				result.Add(action.Name);

			return result;
		}
	}
}