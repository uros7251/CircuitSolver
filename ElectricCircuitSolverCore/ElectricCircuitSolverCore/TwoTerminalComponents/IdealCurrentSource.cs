using System;
using System.Collections.Generic;
using System.Text;
using ElectricCircuitSolverCore.CurrentVoltageCharacteristic;
using ElectricCircuitSolverCore.TwoTerminalComponents.Abstract;
using System.Numerics;
using ElectricCircuitSolverCore.TwoTerminalComponents.Interface;

namespace ElectricCircuitSolverCore.TwoTerminalComponents
{
	public class IdealCurrentSource : TwoTerminalComponent
	{
		#region Attributes
		private Complex _amperage;
		#endregion

		#region Constructors
		public IdealCurrentSource(string id, Complex amperage, char unit = 'A')
			: base(id)
		{
			_amperage = amperage;
			switch (unit)
			{
				case 'm':
					_amperage /= 1e3;
					break;
				case 'u':
					_amperage /= 1e6;
					break;
				case 'n':
					_amperage /= 1e9;
					break;
				default:
					break;
			}
		}
		public IdealCurrentSource(string id, double magnitude, double phase, char unit = 'A')
			: this(id, Complex.FromPolarCoordinates(magnitude, phase), unit)
		{

		}
		#endregion

		#region Properties
		public override ComponentType Type => ComponentType.IdealCurrentSource;
		#endregion

		#region Methods
		public override LinearCurrentVoltageCharacteristic CalculateCurrentVoltageCharacteristic(double omega)
		{
			return _currentVoltageCharacteristic ?? new LinearCurrentVoltageCharacteristic(false, Complex.One, _amperage);
		}

		public override TwoTerminalComponent Reverse()
		{
			_amperage = -_amperage;
			if (_currentVoltageCharacteristic != null)
				_currentVoltageCharacteristic = ~_currentVoltageCharacteristic;
			return this;
		}
		#endregion
	}
}
