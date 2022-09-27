using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using ElectricCircuitSolverCore.InternationalSystemOfUnits;

namespace ElectricCircuitSolverCore.TwoTerminalComponents.Abstract
{
	public abstract class ComplexValuedComponent : TwoTerminalComponent
	{
		protected Complex _value;
		public ComplexValuedComponent(string label, Complex value, Prefix unit)
			:base(label)
		{
			_value = value * SIUnits.GetPrefixValue(unit);
		}
		public ComplexValuedComponent(string label, double magnitude, double phase, Prefix unit)
			:this(label, Complex.FromPolarCoordinates(magnitude, phase), unit)
		{ }
	}
}
