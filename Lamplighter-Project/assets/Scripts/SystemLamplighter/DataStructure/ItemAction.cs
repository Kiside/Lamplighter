using System;
using Godot;

namespace SystemLamplighter.DataStructure
{
	[GlobalClass]
	public partial class ItemAction : ActionData
	{
		[Export]
		protected ItemType _itemType;
		[Export]
		protected string _description;
		public ItemType ItemType => _itemType;
		public string Description => _description;

		public ItemAction() : this(ItemType.RESTORE, "") { }

		public ItemAction(ItemType itemType, string description)
		{
			_itemType = itemType;
			_description = description;
		}
	}
}