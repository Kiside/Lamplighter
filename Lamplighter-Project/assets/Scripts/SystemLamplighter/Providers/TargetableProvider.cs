using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;

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
	public ITargetable GetTargetable(ITargetable targetable) => _targetables.Find(t => t == targetable);
	public ITargetable GetTargetable(int index) => _targetables[index];
	public int GetIndex(ITargetable targetable) => _targetables.IndexOf(targetable);
	public int Count => _targetables.Count;
}