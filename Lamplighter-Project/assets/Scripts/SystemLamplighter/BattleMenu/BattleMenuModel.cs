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

		public List<ISubMenuDefinition> _menus {get; private set;}

		public override void Init()
		{
			_menus = new List<ISubMenuDefinition>
			{
				new AttackSubMenu(),
				new MagicSubMenu(),
				new ItemSubMenu(),
				new DefendSubMenu()
			};
		}

		public void SetSubMenuActions(IEnumerable<IActionData> actionDatas)
		{
			SubMenuActions = actionDatas.ToList();
		}

		public void ClearSubMenuActions() => SubMenuActions = System.Array.Empty<IActionData>();

		
	}
}