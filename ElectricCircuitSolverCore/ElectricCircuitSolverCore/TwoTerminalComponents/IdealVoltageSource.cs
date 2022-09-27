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
	public class IdealVoltageSource : TwoTerminalComponent
	{
		#region Constructors

		public IdealVoltageSource(string label, Complex emf, Prefix prefix = Prefix.None) : base(label, emf, prefix)
		{
		}
		public IdealVoltageSource(string label, Complex emf, char prefix) : base(label, emf, prefix)
		{
		}
		public IdealVoltageSource(string label, double magnitude, double phase = 0, Prefix prefix = Prefix.None)
			: this(label, Complex.FromPolarCoordinates(magnitude, phase), prefix)
		{
		}
		public IdealVoltageSource(string label, double magnitude, double phase, char prefix)
			: this(label, Complex.FromPolarCoordinates(magnitude, phase), prefix)
		{
		}
		public IdealVoltageSource(string label, double magnitude, char prefix)
			: this(label, Complex.FromPolarCoordinates(magnitude, 0), prefix)
		{
		}
		#endregion

		#region Properties
		public override ComponentType Type => ComponentType.IdealVoltageSource;
		#endregion

		#region Methods
		public override LinearCurrentVoltageCharacteristic CalculateCurrentVoltageCharacteristic(double omega)
		{
			return _currentVoltageCharacteristic ?? new LinearCurrentVoltageCharacteristic(true, Complex.Zero, Value);
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
