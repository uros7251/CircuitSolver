using ElectricCircuitSolverCore.CurrentVoltageCharacteristic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using ElectricCircuitSolverCore.TwoTerminalComponents.Interface;
using ElectricCircuitSolverCore.InternationalSystemOfUnits;

namespace ElectricCircuitSolverCore.TwoTerminalComponents
{
	public class Inductor : TwoTerminalComponents.Abstract.RealValuedComponent
	{
		#region Constructor
		public Inductor(string id, double inductance, Prefix unit = Prefix.None)
			: base(id, inductance * SCALE, unit) // inductance is expressed in milihenries by default
		{
		}
		#endregion

		#region Properties
		private double Inductance => _value;
		public override ComponentType Type => ComponentType.Inductor;
		#endregion

		#region Methods
		public override LinearCurrentVoltageCharacteristic CalculateCurrentVoltageCharacteristic(double omega)
		{
			return omega == 0 ?
				LinearCurrentVoltageCharacteristic.ShortCircuit() :
				new LinearCurrentVoltageCharacteristic(true, new Complex(0, - omega * Inductance / SCALE), Complex.Zero);
		}
		#endregion

		#region Static attributes
		private static readonly double SCALE = 1e+3;
		#endregion
	}
}
