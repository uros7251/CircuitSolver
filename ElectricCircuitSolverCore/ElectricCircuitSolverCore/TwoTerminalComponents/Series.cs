using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using ElectricCircuitSolverCore.CurrentVoltageCharacteristic;
using ElectricCircuitSolverCore.TwoTerminalComponents.Abstract;
using ElectricCircuitSolverCore.TwoTerminalComponents.Interface;

namespace ElectricCircuitSolverCore.TwoTerminalComponents
{
	public class Series : CompositeComponent
	{
		#region Attributes
		private TwoTerminalComponent? _fixedCurrentComponent;
		#endregion

		#region Properties
		public override ComponentType Type => ComponentType.Series;
		#endregion

		#region Constructor
		public Series(string? id = null)
			: base(id)
		{
			_fixedCurrentComponent = null;
		}
		#endregion

		#region Methods
		public override CompositeComponent Add(TwoTerminalComponent component)
		{
			if (component.Type == ComponentType.IdealCurrentSource)
			{
				if (_fixedCurrentComponent == null)
					_fixedCurrentComponent = component;
				else
					throw new Exception();
			}
			else
				_components.Add(component);
			return this;
		}
		public override bool Remove(TwoTerminalComponent component)
		{
			if (component == _fixedCurrentComponent)
			{
				_fixedCurrentComponent = null;
				return true;
			}
			else
				return _components.Remove(component);
		}
		public override LinearCurrentVoltageCharacteristic CalculateCurrentVoltageCharacteristic(double omega)
		{
			if (_fixedCurrentComponent != null)
				return _fixedCurrentComponent.CurrentVoltageCharacteristic(omega);
			if (_components == null)
				return LinearCurrentVoltageCharacteristic.ShortCircuit();

			var characteristic = LinearCurrentVoltageCharacteristic.ShortCircuit();
			foreach (var component in _components)
			{
				characteristic &= component.CurrentVoltageCharacteristic(omega);
			}
			return characteristic;
		}

		public override void ApplyCurrent(Complex current, double omega)
		{
			base.ApplyCurrent(current, omega);

			foreach (var component in _components)
			{
				component.ApplyCurrent(current, omega);
			}
			if (_fixedCurrentComponent != null)
			{
				Complex voltageDrop = Complex.Zero;
				foreach (var component in _components)
				{
					voltageDrop += component.State.Voltage;
				}
				_fixedCurrentComponent.ApplyVoltage(_state.Voltage - voltageDrop, omega);
			}
		}

		public override void ApplyVoltage(Complex voltage, double omega)
		{
			base.ApplyVoltage(voltage, omega);
			foreach (var component in _components)
			{
				component.ApplyCurrent(_state.Current, omega);
			}
			if (_fixedCurrentComponent != null)
			{
				Complex voltageDrop = Complex.Zero;
				foreach (var component in _components)
				{
					voltageDrop += component.State.Voltage;
				}
				_fixedCurrentComponent.ApplyVoltage(_state.Voltage - voltageDrop, omega);
			}
		}
		public override TwoTerminalComponent InSeriesWith(TwoTerminalComponent other)
		{
			if (other is Series)
			{
				var otherAsSeries = (Series)other;
				foreach (var component in otherAsSeries.Components)
				{
					Add(component);
				}
			}
			else
			{
				Add(other);
			}
			return this;
		}
		public override TwoTerminalComponent Reverse()
		{
			foreach(var component in _components)
			{
				component.Reverse();
			}
			if (_currentVoltageCharacteristic != null)
				_currentVoltageCharacteristic = ~_currentVoltageCharacteristic;
			return this;
		}
		#endregion
	}
}
