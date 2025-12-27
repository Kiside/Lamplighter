using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SystemLamplighter;
using SystemLamplighter.DataStructure;
using SystemLamplighter.Debug;

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

		[Export]
		public bool startHide {get; private set;} = true;

		private List<string> _attacks;

		private List<string> _magics;
		private List<string> _items;

		public override void Init()
		{

		}

		public void Init(List<string> attacks, List<string> magics, List<string> items)
		{
			DebugLamplighter.Assert(attacks != null, "can't initialize _attacks because the list is null");
			DebugLamplighter.Assert(magics != null, "can't initialize _magics because the list is null");
			DebugLamplighter.Assert(items != null, "can't initialize _items because the list is null");
			DebugLamplighter.Assert(attacks.Count > 0, "can't initialize _attacks because the list is less then zero");
			DebugLamplighter.Assert(magics.Count > 0, "can't initialize _magics because the list is less then zero");
			DebugLamplighter.Assert(items.Count > 0, "can't initialize _items because the list is less then zero");

			_attacks = attacks;
			_magics = magics;
			_items = items;
		}

		
		
	}
}