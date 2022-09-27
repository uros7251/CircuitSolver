using ElectricCircuitSolverCore.CurrentVoltageCharacteristic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using ElectricCircuitSolverCore.TwoTerminalComponents.Interface;
using ElectricCircuitSolverCore.InternationalSystemOfUnits;

namespace ElectricCircuitSolverCore.TwoTerminalComponents
{
	public class Capacitor : TwoTerminalComponents.Abstract.TwoTerminalComponent
	{
		#region Constructors
		public Capacitor(string label, double capacitance, Prefix prefix = Prefix.None) : base(label, new Complex(0,-1/capacitance), prefix)
		{
		}
		public Capacitor(string label, double capacitance, char prefix) : base(label, new Complex(0,-1/capacitance), prefix)
		{
		}
		#endregion
		#region Properties
		public override ComponentType Type => ComponentType.Capacitor;
		#endregion

		#region Methods
		public override LinearCurrentVoltageCharacteristic CalculateCurrentVoltageCharacteristic(double omega)
		{
			return omega == 0 ?
				LinearCurrentVoltageCharacteristic.OpenCircuit() :
				new LinearCurrentVoltageCharacteristic(true, -Value/omega, Complex.Zero);
		}
		#endregion
	}
}
