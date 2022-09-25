using System;
using System.Collections.Generic;
using System.Text;
using ElectricCircuitSolverCore.CurrentVoltageCharacteristic;
using ElectricCircuitSolverCore.TwoTerminalComponents.Abstract;
using System.Numerics;
using ElectricCircuitSolverCore.TwoTerminalComponents.Interface;

namespace ElectricCircuitSolverCore.TwoTerminalComponents
{
	public class IdealVoltageSource : TwoTerminalComponent
	{
		#region Attributes
		private Complex _emf;
		#endregion

		#region Constructors
		public IdealVoltageSource(string id, Complex emf, char unit = 'V')
			: base(id)
		{
			_emf = emf;
			switch (unit)
			{
				case 'k':
					_emf *= 1e3;
					break;
				case 'm':
					_emf /= 1e3;
					break;
				default:
					break;
			}
		}
		public IdealVoltageSource(string id, double magnitude, double phase = 0, char unit = 'V')
			: this(id, Complex.FromPolarCoordinates(magnitude, phase), unit)
		{

		}
		#endregion

		#region Properties
		public override ComponentType Type => ComponentType.IdealVoltageSource;
		#endregion

		#region Methods
		public override LinearCurrentVoltageCharacteristic CalculateCurrentVoltageCharacteristic(double omega)
		{
			return _currentVoltageCharacteristic ?? new LinearCurrentVoltageCharacteristic(true, Complex.Zero, _emf);
		}

		public override TwoTerminalComponent Reverse()
		{
			_emf = -_emf;
			if (_currentVoltageCharacteristic != null)
				_currentVoltageCharacteristic = ~_currentVoltageCharacteristic;
			return this;
		}
		#endregion
	}
}
