using System.Collections.Generic;
using Godot;
using SystemLamplighter.Common.Enums;

public interface ITargetableProvider
{
	public void Init(List<ITargetable> targetables);
	public void AddTargetable(ITargetable targetable);
	public void RemoveTargetable(ITargetable targetable);
	public void Clear();
	public List<ITargetable> GetTargetables();
	public List<ITargetable> GetTargetables(List<TargeTableType> type);
	public ITargetable GetTargetable(string targetableName);
	public ITargetable GetTargetable(ITargetable targetable);
	public ITargetable GetTargetable(int index);
	public ITargetable GetTargetable(int index, List<TargeTableType> type);
	public int GetIndex(ITargetable targetable);
	public int GetIndex(ITargetable targetable, List<TargeTableType> type);
	public int Count {get;}
	public int CountOf(List<TargeTableType> type);
}