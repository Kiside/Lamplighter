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
		[Export]
		public bool startHide {get; private set;} = true;

		public IReadOnlyList<IActionData> SubMenuActions {get; private set;}

		private List<string> _attacks;

		private List<string> _magics;
		private List<string> _items;

		public override void Init()
		{

		}

		public void SetSubMenuActions(IEnumerable<IActionData> actionDatas)
		{
			SubMenuActions = actionDatas.ToList();
		}

		public void ClearSubMenuActions() => SubMenuActions = System.Array.Empty<IActionData>();

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