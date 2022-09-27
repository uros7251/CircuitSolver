using ElectricCircuitSolverCore.CurrentVoltageCharacteristic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using ElectricCircuitSolverCore.TwoTerminalComponents.Interface;
using ElectricCircuitSolverCore.InternationalSystemOfUnits;

namespace ElectricCircuitSolverCore.TwoTerminalComponents
{
	public class Impedance : Abstract.TwoTerminalComponent
	{
		#region Constructors
		public Impedance(string label, Complex impendace, Prefix prefix = Prefix.None) : base(label, impendace, prefix)
		{
		}
		public Impedance(string label, Complex impendace, char prefix) : base(label, impendace, prefix)
		{
		}
		#endregion

		#region Properties
		public override ComponentType Type => ComponentType.Impedance;
		#endregion

		#region Methods
		public override LinearCurrentVoltageCharacteristic CalculateCurrentVoltageCharacteristic(double omega)
		{
			return _currentVoltageCharacteristic ?? new LinearCurrentVoltageCharacteristic(true, omega * Value, Complex.Zero);
		}
		#endregion
	}
}
