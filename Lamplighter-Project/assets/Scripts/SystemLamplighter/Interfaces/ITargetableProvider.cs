using System.Collections.Generic;
using Godot;

public interface ITargetableProvider
{
	public void Init(List<ITargetable> targetables);
	public void AddTargetable(ITargetable targetable);
	public void RemoveTargetable(ITargetable targetable);
	public void Clear();
	public List<ITargetable> GetTargetables();
	public ITargetable GetTargetable(ITargetable targetable);
	public ITargetable GetTargetable(int index);
	public int GetIndex(ITargetable targetable);
	public int Count {get;}
}