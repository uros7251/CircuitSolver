using ElectricCircuitSolverCore.CurrentVoltageCharacteristic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using ElectricCircuitSolverCore.TwoTerminalComponents.Interface;
using ElectricCircuitSolverCore.InternationalSystemOfUnits;

namespace ElectricCircuitSolverCore.TwoTerminalComponents
{
	public class Resistor : Abstract.RealValuedComponent
	{
		#region Constructors
		public Resistor(string id, double resistance, Prefix unit = Prefix.None)
			: base(id, resistance, unit)
		{
		}
		#endregion

		#region Properties
		private double Resistance => _value;
		public override ComponentType Type => ComponentType.Resistor;
		#endregion

		#region Methods
		public override LinearCurrentVoltageCharacteristic CalculateCurrentVoltageCharacteristic(double omega)
		{
			return _currentVoltageCharacteristic ?? new LinearCurrentVoltageCharacteristic(true, new Complex(-Resistance, 0), Complex.Zero);
		}
		#endregion
	}
}
