using System;
using System.Collections.Generic;
using System.Text;
using ElectricCircuitSolverCore.CurrentVoltageCharacteristic;
using ElectricCircuitSolverCore.TwoTerminalComponents.Abstract;
using System.Numerics;
using ElectricCircuitSolverCore.TwoTerminalComponents.Interface;
using ElectricCircuitSolverCore.InternationalSystemOfUnits;

namespace ElectricCircuitSolverCore.TwoTerminalComponents
{
	public class IdealCurrentSource : TwoTerminalComponent
	{
		#region Constructors

		public IdealCurrentSource(string label, Complex amperage, Prefix prefix = Prefix.None) : base(label, amperage, prefix)
		{
		}
		public IdealCurrentSource(string label, Complex amperage, char prefix) : base(label, amperage, prefix)
		{
		}
		public IdealCurrentSource(string label, double magnitude, double phase = 0, Prefix prefix = Prefix.None)
			: this(label, Complex.FromPolarCoordinates(magnitude, phase), prefix)
		{
		}
		public IdealCurrentSource(string label, double magnitude, double phase, char prefix)
			: this(label, Complex.FromPolarCoordinates(magnitude, phase), prefix)
		{
		}
		public IdealCurrentSource(string label, double magnitude, char prefix)
			: this(label, Complex.FromPolarCoordinates(magnitude, 0), prefix)
		{
		}
		#endregion

		#region Properties
		public override ComponentType Type => ComponentType.IdealCurrentSource;
		#endregion

		#region Methods
		public override LinearCurrentVoltageCharacteristic CalculateCurrentVoltageCharacteristic(double omega)
		{
			return _currentVoltageCharacteristic ?? new LinearCurrentVoltageCharacteristic(false, Complex.One, Value);
		}

		public override TwoTerminalComponent Reverse()
		{
			Value = -Value;
			if (_currentVoltageCharacteristic != null)
				_currentVoltageCharacteristic = ~_currentVoltageCharacteristic;
			return this;
		}
		#endregion
	}
}
