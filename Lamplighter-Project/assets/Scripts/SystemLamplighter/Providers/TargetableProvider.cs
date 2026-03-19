using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using SystemLamplighter.Common.Enums;
using SystemLamplighter.Tool;

public class TargetableProvider : ITargetableProvider
{
	private List<ITargetable> _targetables;

	public void Init(List<ITargetable> targetables)
	{
		if(_targetables is null)
			_targetables = new List<ITargetable>();

		_targetables = targetables;
	}
	public void AddTargetable(ITargetable targetable) => _targetables.Add(targetable);
	
	public void RemoveTargetable(ITargetable targetable) => _targetables.Remove(targetable);
	public void Clear() => _targetables.Clear();
	public List<ITargetable> GetTargetables() => _targetables;
	public List<ITargetable> GetTargetables(List<TargeTableType> type)
	{
		var typeSet = new HashSet<TargeTableType>(type);
    	return _targetables.Where(t => typeSet.Contains(t.TargeTableType)).ToList();
	} 
	public ITargetable GetTargetable(string targetableName) => _targetables.Find(t => t.TargetableName == targetableName);
	public ITargetable GetTargetable(ITargetable targetable) => _targetables.Find(t => t == targetable);
	public ITargetable GetTargetable(int index) => _targetables[index];
	public ITargetable GetTargetable(int index, List<TargeTableType> type)
	{
		var typeSet = new HashSet<TargeTableType>(type);
    	return _targetables.Where(t => typeSet.Contains(t.TargeTableType)).ToList()[index];
	}
	public int GetIndex(ITargetable targetable) => _targetables.IndexOf(targetable);
	public int GetIndex(ITargetable targetable, List<TargeTableType> type)
	{
		var typeSet = new HashSet<TargeTableType>(type);
    	return _targetables.Where(t => typeSet.Contains(t.TargeTableType)).ToList().IndexOf(targetable);
	}
	public int Count => _targetables.Count;
	public int CountOf(List<TargeTableType> type)
	{
		var typeSet = new HashSet<TargeTableType>(type);
    	return _targetables.Where(t => typeSet.Contains(t.TargeTableType)).ToList().Count;
	}
}