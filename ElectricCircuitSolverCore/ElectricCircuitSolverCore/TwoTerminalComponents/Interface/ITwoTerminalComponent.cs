using ElectricCircuitSolverCore.CurrentVoltageCharacteristic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace ElectricCircuitSolverCore.TwoTerminalComponents.Interface
{
	public enum ComponentType
	{
		IdealVoltageSource,
		IdealCurrentSource,
		Resistor,
		Capacitor,
		Inductor,
		Impedance,
		Parallel,
		Series
	};
	public interface ITwoTerminalComponent
	{
		ComponentType Type { get; }
		LinearCurrentVoltageCharacteristic CurrentVoltageCharacteristic(double omega = 0);
		void ApplyCurrent(Complex current, double omega = 0);
		void ApplyVoltage(Complex voltage, double omega = 0);
		CurrentVoltageState State { get; }
	}
}
