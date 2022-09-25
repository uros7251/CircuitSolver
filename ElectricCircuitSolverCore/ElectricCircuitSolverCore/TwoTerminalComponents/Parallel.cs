using System;
using System.Collections.Generic;
using System.Text;
using ElectricCircuitSolverCore.TwoTerminalComponents.Interface;
using ElectricCircuitSolverCore.TwoTerminalComponents.Abstract;
using ElectricCircuitSolverCore.CurrentVoltageCharacteristic;
using System.Numerics;

namespace ElectricCircuitSolverCore.TwoTerminalComponents
{
	public class Parallel : TwoTerminalComponent
	{

		#region Attributes
		private TwoTerminalComponent _fixedVoltageComponent;
		private List<TwoTerminalComponent> _components;
		#endregion

		#region Properties
		public IReadOnlyCollection<TwoTerminalComponent> Components
		{
			get => _components.AsReadOnly();
		}
		public override ComponentType Type => ComponentType.Parallel;

		#endregion

		#region Constructor
		public Parallel(string id = null)
			: base(id)
		{
			_fixedVoltageComponent = null;
			_components = new List<TwoTerminalComponent>();
		}
		#endregion

		#region Methods
		public Parallel Add(TwoTerminalComponent component)
		{
			if (component.CurrentVoltageCharacteristic().HasFixedVoltage)
			{
				if (_fixedVoltageComponent == null)
					_fixedVoltageComponent = component;
				else
					throw new Exception();
			}
			else
				_components.Add(component);
			return this;
		}
		public bool Remove(TwoTerminalComponent component)
		{
			if (component == _fixedVoltageComponent)
			{
				_fixedVoltageComponent = null;
				return true;
			}
			else
				return _components.Remove(component);
		}
		public override LinearCurrentVoltageCharacteristic CalculateCurrentVoltageCharacteristic(double omega)
		{
			if (_fixedVoltageComponent != null)
				return _fixedVoltageComponent.CurrentVoltageCharacteristic(omega);
			if (_components == null)
				return LinearCurrentVoltageCharacteristic.OpenCircuit();

			var characteristic = LinearCurrentVoltageCharacteristic.OpenCircuit();
			foreach (var component in _components)
			{
				characteristic |= component.CurrentVoltageCharacteristic(omega);
			}
			return characteristic;
		}

		public override void ApplyCurrent(Complex current, double omega)
		{
			base.ApplyCurrent(current, omega);
			foreach (var component in _components)
			{
				component.ApplyVoltage(_state.Voltage, omega);
			}
			if (_fixedVoltageComponent != null)
			{
				Complex totalCurrent = Complex.Zero;
				foreach (var component in _components)
				{
					totalCurrent += component.State.Current;
				}
				_fixedVoltageComponent.ApplyCurrent(_state.Current - totalCurrent, omega);
			}
		}

		public override void ApplyVoltage(Complex voltage, double omega)
		{
			base.ApplyVoltage(voltage, omega);
			foreach (var component in _components)
			{
				component.ApplyVoltage(voltage, omega);
			}
			if (_fixedVoltageComponent != null)
			{
				Complex totalCurrent = Complex.Zero;
				foreach (var component in _components)
				{
					totalCurrent += component.State.Current;
				}
				_fixedVoltageComponent.ApplyCurrent(_state.Current - totalCurrent, omega);
			}
		}
		public override TwoTerminalComponent InParallelWith(TwoTerminalComponent other)
		{
			if (other is Parallel)
			{
				var otherAsParallel = (Parallel)other;
				foreach (var component in otherAsParallel.Components)
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
