using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Text.Json;

namespace ElectricCircuitSolverCore.CurrentVoltageCharacteristic
{
	public struct CurrentVoltageState
	{
		public Complex
			Voltage,
			Current;
		public override string ToString()
		{
			return $"Voltage: {Voltage}, Current: {Current}";
		}
		public static CurrentVoltageState operator -(CurrentVoltageState currentVoltageState)
			=> new CurrentVoltageState
			{
				Voltage = -currentVoltageState.Voltage,
				Current = -currentVoltageState.Current
			};
	}
}
