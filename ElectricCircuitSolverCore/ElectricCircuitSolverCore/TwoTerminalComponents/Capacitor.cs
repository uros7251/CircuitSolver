using ElectricCircuitSolverCore.CurrentVoltageCharacteristic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using ElectricCircuitSolverCore.TwoTerminalComponents.Interface;

namespace ElectricCircuitSolverCore.TwoTerminalComponents
{
	public class Capacitor : TwoTerminalComponents.Abstract.TwoTerminalComponent
	{
		#region Attributes
		private double _capacitance;
		#endregion

		#region Constructor
		public Capacitor(string label, double capacitance, char unit = 'n')
			: base(label)
		{
			_capacitance = capacitance;
			switch (unit)
			{
				case 'F':
					_capacitance *= 1e+9;
					break;
				case 'm':
					_capacitance *= 1e+6;
					break;
				case 'u':
					_capacitance *= 1e+3;
					break;
				case 'p':
					_capacitance /= 1e+3;
					break;
				default:
					break;
			}
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
				new LinearCurrentVoltageCharacteristic(true, new Complex(0, SCALE / (omega * _capacitance)), Complex.Zero);
		}
		#endregion

		#region Static attributes
		private static readonly double SCALE = 1e+9;
		#endregion
	}
}
