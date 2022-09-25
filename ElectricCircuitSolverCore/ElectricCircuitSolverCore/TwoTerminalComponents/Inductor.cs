using ElectricCircuitSolverCore.CurrentVoltageCharacteristic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using ElectricCircuitSolverCore.TwoTerminalComponents.Interface;

namespace ElectricCircuitSolverCore.TwoTerminalComponents
{
	public class Inductor : TwoTerminalComponents.Abstract.TwoTerminalComponent
	{
		#region Attributes
		private double _inductance;
		#endregion

		#region Constructor
		public Inductor(string id, double inductance, char unit = 'm')
			: base(id)
		{
			_inductance = inductance;
			switch (unit)
			{
				case 'H':
					_inductance *= 1e3;
					break;
				case 'u':
					_inductance /= 1e3;
					break;
				case 'p':
					_inductance /= 1e6;
					break;
				default:
					break;
			}
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
				new LinearCurrentVoltageCharacteristic(true, new Complex(0, - omega * _inductance / SCALE), Complex.Zero);
		}
		#endregion

		#region Static attributes
		private static readonly double SCALE = 1e+3;
		#endregion
	}
}
