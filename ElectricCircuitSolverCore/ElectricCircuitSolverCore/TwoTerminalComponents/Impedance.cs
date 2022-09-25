using ElectricCircuitSolverCore.CurrentVoltageCharacteristic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using ElectricCircuitSolverCore.TwoTerminalComponents.Interface;

namespace ElectricCircuitSolverCore.TwoTerminalComponents
{
	public class Impedance : Abstract.TwoTerminalComponent
	{
		#region Attributes
		private Complex _impedance;
		#endregion

		#region Constructors
		public Impedance(string id, Complex impedance, char unit = 'O')
			: base(id)
		{
			_impedance = impedance;
			switch (unit)
			{
				case 'm':
					_impedance /= 1e3;
					break;
				case 'k':
					_impedance *= 1e3;
					break;
				case 'M':
					_impedance *= 1e6;
					break;
				default:
					break;
			}
		}
		#endregion

		#region Properties
		public override ComponentType Type => ComponentType.Impedance;
		#endregion

		#region Methods
		public override LinearCurrentVoltageCharacteristic CalculateCurrentVoltageCharacteristic(double omega)
		{
			return _currentVoltageCharacteristic ?? new LinearCurrentVoltageCharacteristic(true, _impedance, Complex.Zero);
		}
		#endregion
	}
}
