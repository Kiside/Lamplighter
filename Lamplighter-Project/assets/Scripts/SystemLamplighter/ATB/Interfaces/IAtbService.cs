using System;
using System.Collections.Generic;
using Characters.Interfaces;

namespace SystemLamplighter.ATB.Interfaces
{
	public interface IAtbService : IDisposable
	{
		public void ActivateATB(bool value);
		public bool ClockingAtb(double delta);
	}
}