using ElectricCircuitSolverCore.CurrentVoltageCharacteristic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using ElectricCircuitSolverCore.TwoTerminalComponents.Interface;
using ElectricCircuitSolverCore.InternationalSystemOfUnits;

namespace ElectricCircuitSolverCore.TwoTerminalComponents
{
	public class Impedance : Abstract.ComplexValuedComponent
	{
		#region Constructors
		public Impedance(string id, Complex impedance, Prefix unit = Prefix.None)
			: base(id, impedance, unit)
		{
		}
		#endregion

		#region Properties
		private Complex Z => _value;
		public override ComponentType Type => ComponentType.Impedance;
		#endregion

		#region Methods
		public override LinearCurrentVoltageCharacteristic CalculateCurrentVoltageCharacteristic(double omega)
		{
			return _currentVoltageCharacteristic ?? new LinearCurrentVoltageCharacteristic(true, Z, Complex.Zero);
		}
		#endregion
	}
}
