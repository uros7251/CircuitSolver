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
	public class IdealVoltageSource : ComplexValuedComponent
	{

		#region Constructors
		public IdealVoltageSource(string id, Complex emf, Prefix unit = Prefix.None)
			: base(id, emf, unit)
		{
		}
		public IdealVoltageSource(string id, double magnitude, double phase = 0, Prefix unit = Prefix.None)
			: base(id, magnitude, phase, unit)
		{
		}
		#endregion

		#region Properties
		private Complex Emf
		{
			get { return _value; }
			set { _value = value; }
		}
		public override ComponentType Type => ComponentType.IdealVoltageSource;
		#endregion

		#region Methods
		public override LinearCurrentVoltageCharacteristic CalculateCurrentVoltageCharacteristic(double omega)
		{
			return _currentVoltageCharacteristic ?? new LinearCurrentVoltageCharacteristic(true, Complex.Zero, Emf);
		}

		public override TwoTerminalComponent Reverse()
		{
			Emf = -Emf;
			if (_currentVoltageCharacteristic != null)
				_currentVoltageCharacteristic = ~_currentVoltageCharacteristic;
			return this;
		}
		#endregion
	}
}
