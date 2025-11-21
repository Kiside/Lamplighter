using Godot;
using System;
using System.Collections.Generic;
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

		public override void Init()
		{
			throw new NotImplementedException();
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

		private List<string> GetNames<[MustBeVariant] T>(Godot.Collections.Array<T> list) where T : ActionData
		{
			var result = new List<string>(list.Count);
			foreach (var action in list)
				result.Add(action.Name);

			return result;
		}
	}
}