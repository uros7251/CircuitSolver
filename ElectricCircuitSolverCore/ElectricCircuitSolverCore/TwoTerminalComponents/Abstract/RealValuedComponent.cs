using ElectricCircuitSolverCore.CurrentVoltageCharacteristic;
using ElectricCircuitSolverCore.InternationalSystemOfUnits;
using ElectricCircuitSolverCore.TwoTerminalComponents.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCircuitSolverCore.TwoTerminalComponents.Abstract
{
	public abstract class RealValuedComponent : TwoTerminalComponent
	{
		protected double _value;
		public RealValuedComponent(string label, double value, Prefix unit)
			:base(label)
		{
			_value = value * SIUnits.GetPrefixValue(unit);
		}
	}
}
