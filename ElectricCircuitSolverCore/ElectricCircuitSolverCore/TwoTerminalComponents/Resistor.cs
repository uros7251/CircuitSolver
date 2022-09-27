using ElectricCircuitSolverCore.CurrentVoltageCharacteristic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using ElectricCircuitSolverCore.TwoTerminalComponents.Interface;
using ElectricCircuitSolverCore.InternationalSystemOfUnits;

namespace ElectricCircuitSolverCore.TwoTerminalComponents
{
	public class Resistor : Abstract.TwoTerminalComponent
	{
		#region Constructors

		public Resistor(string label, double resistance, Prefix prefix = Prefix.None) : base(label, new Complex(resistance, 0), prefix)
		{
		}
		public Resistor(string label, double resistance, char prefix) : base(label, new Complex(resistance, 0), prefix)
		{
		}
		#endregion

		#region Properties
		public override ComponentType Type => ComponentType.Capacitor;
		#endregion

		#region Methods
		public override LinearCurrentVoltageCharacteristic CalculateCurrentVoltageCharacteristic(double omega)
		{
			return _currentVoltageCharacteristic ?? new LinearCurrentVoltageCharacteristic(true, -Value, Complex.Zero);
		}
		#endregion
	}
}
