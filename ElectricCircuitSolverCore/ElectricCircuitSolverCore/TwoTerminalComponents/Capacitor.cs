using ElectricCircuitSolverCore.CurrentVoltageCharacteristic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using ElectricCircuitSolverCore.TwoTerminalComponents.Interface;
using ElectricCircuitSolverCore.InternationalSystemOfUnits;

namespace ElectricCircuitSolverCore.TwoTerminalComponents
{
	public class Capacitor : TwoTerminalComponents.Abstract.RealValuedComponent
	{
		#region Constructor
		public Capacitor(string label, double capacitance, Prefix unit = Prefix.None)
			: base(label, capacitance * SCALE, unit) // capacitance is expressed in nanofarads by default
		{
		}
		#endregion

		#region Properties
		private double Capacitance => _value;
		public override ComponentType Type => ComponentType.Capacitor;
		#endregion

		#region Methods
		public override LinearCurrentVoltageCharacteristic CalculateCurrentVoltageCharacteristic(double omega)
		{
			return omega == 0 ?
				LinearCurrentVoltageCharacteristic.OpenCircuit() :
				new LinearCurrentVoltageCharacteristic(true, new Complex(0, SCALE / (omega * Capacitance)), Complex.Zero);
		}
		#endregion

		#region Static attributes
		private static readonly double SCALE = 1e+9;
		#endregion
	}
}
