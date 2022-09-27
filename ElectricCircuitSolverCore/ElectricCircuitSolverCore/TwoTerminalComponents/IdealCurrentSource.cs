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
	public class IdealCurrentSource : ComplexValuedComponent
	{
		#region Constructors
		public IdealCurrentSource(string id, Complex amperage, Prefix unit = Prefix.None)
			: base(id, amperage, unit)
		{
		}
		public IdealCurrentSource(string id, double magnitude, double phase, Prefix unit = Prefix.None)
			: base(id, magnitude, phase, unit)
		{
		}
		#endregion

		#region Properties
		private Complex Amperage
		{
			get { return _value; }
			set { _value = value; }
		}
		public override ComponentType Type => ComponentType.IdealCurrentSource;
		#endregion

		#region Methods
		public override LinearCurrentVoltageCharacteristic CalculateCurrentVoltageCharacteristic(double omega)
		{
			return _currentVoltageCharacteristic ?? new LinearCurrentVoltageCharacteristic(false, Complex.One, Amperage);
		}

		public override TwoTerminalComponent Reverse()
		{
			Amperage = -Amperage;
			if (_currentVoltageCharacteristic != null)
				_currentVoltageCharacteristic = ~_currentVoltageCharacteristic;
			return this;
		}
		#endregion
	}
}
