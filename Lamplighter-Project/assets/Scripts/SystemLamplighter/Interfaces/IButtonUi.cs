
using System;

namespace SystemLamplighter.Interfaces
{
	/// <summary>
	/// Interfaccia per i pulsanti
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IButtonUi<T>
	{
		public event Action<T> OnClick;
	}
}