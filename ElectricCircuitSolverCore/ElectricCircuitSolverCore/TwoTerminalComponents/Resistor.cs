using ElectricCircuitSolverCore.CurrentVoltageCharacteristic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using ElectricCircuitSolverCore.TwoTerminalComponents.Interface;

namespace ElectricCircuitSolverCore.TwoTerminalComponents
{
	public class Resistor : Abstract.TwoTerminalComponent
	{
		#region Attributes
		private double _resistance;
		#endregion

		#region Constructors
		public Resistor(string id, double resistance, char unit = 'O')
			: base(id)
		{
			_resistance = resistance;
			switch (unit)
			{
				case 'm':
					_resistance /= 1e3;
					break;
				case 'k':
					_resistance *= 1e3;
					break;
				case 'M':
					_resistance *= 1e6;
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
			return _currentVoltageCharacteristic ?? new LinearCurrentVoltageCharacteristic(true, new Complex(-_resistance, 0), Complex.Zero);
		}
		#endregion
	}
}
