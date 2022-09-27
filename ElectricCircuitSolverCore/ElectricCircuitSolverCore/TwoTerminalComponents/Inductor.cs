using ElectricCircuitSolverCore.CurrentVoltageCharacteristic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using ElectricCircuitSolverCore.TwoTerminalComponents.Interface;
using ElectricCircuitSolverCore.InternationalSystemOfUnits;

namespace ElectricCircuitSolverCore.TwoTerminalComponents
{
	public class Inductor : TwoTerminalComponents.Abstract.TwoTerminalComponent
	{
		#region Constructors

		public Inductor(string label, double inductance, Prefix prefix = Prefix.None) : base(label, new Complex(0, inductance), prefix)
		{
		}
		public Inductor(string label, double inductance, char prefix) : base(label, new Complex(0, inductance), prefix)
		{
		}
		#endregion

		#region Properties
		public override ComponentType Type => ComponentType.Inductor;
		#endregion

		#region Methods
		public override LinearCurrentVoltageCharacteristic CalculateCurrentVoltageCharacteristic(double omega)
		{
			return omega == 0 ?
				LinearCurrentVoltageCharacteristic.ShortCircuit() :
				new LinearCurrentVoltageCharacteristic(true, -Value * omega, Complex.Zero);
		}
		#endregion
	}
}
