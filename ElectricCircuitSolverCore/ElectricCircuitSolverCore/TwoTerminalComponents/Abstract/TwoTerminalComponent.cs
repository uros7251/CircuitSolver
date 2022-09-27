using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using ElectricCircuitSolverCore.CurrentVoltageCharacteristic;
using ElectricCircuitSolverCore.TwoTerminalComponents.Interface;

namespace ElectricCircuitSolverCore.TwoTerminalComponents.Abstract
{
	public abstract class TwoTerminalComponent : ITwoTerminalComponent
	{
		#region Attributes
		protected LinearCurrentVoltageCharacteristic? _currentVoltageCharacteristic;
		protected CurrentVoltageState _state;
		protected double _omega;
		#endregion

		#region Properties
		public string? Label { get; set; }
		public CurrentVoltageState State { get => _state; }
		public Complex Current { get => _state.Current; }
		public Complex Voltage { get => _state.Voltage; }
		public Complex Power { get => _state.Current * _state.Voltage; }
		public abstract ComponentType Type { get; }
		#endregion

		#region Constructors
		public TwoTerminalComponent(string? label)
		{
			Label = label;
			_omega = -1;
		}
		#endregion

		#region Methods
		public LinearCurrentVoltageCharacteristic CurrentVoltageCharacteristic(double omega = 0)
		{
			if (_currentVoltageCharacteristic == null || _omega != omega)
			{
				_omega = omega;
				_currentVoltageCharacteristic =  CalculateCurrentVoltageCharacteristic(omega);
			}
			return _currentVoltageCharacteristic;
		}
		public abstract LinearCurrentVoltageCharacteristic CalculateCurrentVoltageCharacteristic(double omega);

		public virtual void ApplyCurrent(Complex current, double omega)
		{
			_state = new CurrentVoltageState
			{
				Current = current,
				Voltage = CurrentVoltageCharacteristic(omega).VoltageAtCurrent(current)
			};
		}

		public virtual void ApplyVoltage(Complex voltage, double omega)
		{
			_state = new CurrentVoltageState
			{
				Current = CurrentVoltageCharacteristic(omega).CurrentAtVoltage(voltage),
				Voltage = voltage
			};
		}

		public virtual TwoTerminalComponent InSeriesWith(TwoTerminalComponent other)
		{
			if (other is Series)
				return other.InSeriesWith(this);
			return new Series().Add(this).Add(other);
		}
		public virtual TwoTerminalComponent InParallelWith(TwoTerminalComponent other)
		{
			if (other is Parallel)
				return other.InSeriesWith(this);
			return new Parallel().Add(this).Add(other);
		}
		public virtual TwoTerminalComponent Reverse() => this;
		#endregion

		#region Operators

		public static TwoTerminalComponent operator ~(TwoTerminalComponent component) => component.Reverse();
		public static TwoTerminalComponent operator &(TwoTerminalComponent left, TwoTerminalComponent right) => left.InSeriesWith(right);
		public static TwoTerminalComponent operator |(TwoTerminalComponent left, TwoTerminalComponent right) => left.InParallelWith(right);
		#endregion
	}
}
