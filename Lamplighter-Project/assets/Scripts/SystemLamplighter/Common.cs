using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using Godot;
using System.Linq;

namespace SystemLamplighter
{


	public static class Common
	{
		public const float ATB_COM_POSITION = 0.58f;
		public const float ATB_COMAND_THRESHOLD = 0.45F;
		public const float ATB_END = 1f;

		public static float ToSingle(double value)
		{
			return (float)value;
		}

		public static List<string> FromEnumtoList<T>() => Enum.GetNames(typeof(T)).ToList();

	}

	[Serializable]
	public enum GroupsName
	{
		ally,
		enemy,
		neutral_npc
	}

	public static class InputMap
	{
		public const string Up = "move_up";
		public const string Down = "move_down";
		public const string Left = "move_left";
		public const string Right = "move_right";

	}

	public enum AtbCharacterType
	{
		ALLY,
		ENEMY
	}

	public enum AtbCharacterStatus
	{
		CHARGE,
		COM,
		CHARGE_ACTION,
		ACTION
	}

	public enum TargetType
	{
		SELF,
		SINGLE,
		CIRCLE,
		LINE,
		CONE,
		GROUP
	}

	public enum ActionType
	{
		ATTACK,
		GUARD,
		MAGIC,
		ITEM,
		ESCAPE
	}

	public enum AttackType
	{
		LIGHT,
		HEAVY,
		RANGED,
		PUSH,
		AREA
	}

	public enum MagicType
	{
		DAMAGE,
		RESTORE,
		AREA
	}

	public enum ItemType
	{
		RESTORE,
		REVIVE,
		THROW
	}

	public enum SubMenuType
	{
		NONE,
		ATTACK,
		DEFEND,
		MAGIC,
		ITEMS
	}

	
}
