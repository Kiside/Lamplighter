
using System;

namespace SystemLamplighter.Interfaces
{
	public interface IButtonUi<T>
	{
		public event Action<T> OnClick;
	}
}