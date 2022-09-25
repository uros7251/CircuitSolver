using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace ElectricCircuitSolverCore.CurrentVoltageCharacteristic
{
	/* linear characteristic of the form a * v + b * i = c */
	public class LinearCurrentVoltageCharacteristic
	{
		#region Attributes
		protected bool _a;
		protected Complex _b, _c;
		#endregion

		#region Constructors
		public LinearCurrentVoltageCharacteristic(bool a, Complex b, Complex c)
		{
			_a = a;
			_b = b;
			_c = c;
		}
		#endregion

		#region Properties
		public bool HasFixedCurrent
		{
			get { return !_a; }
		}
		public bool HasFixedVoltage
		{
			get { return _a && _b.Magnitude == 0; }
		}

		public Complex ImpedanceCoefficient { get => -_b; }
		public Complex FreeCoefficient { get => _c; }
		#endregion

		#region Methods
		public override string ToString()
		{
			if (HasFixedCurrent)
			{
				return $"I = {_c}";
			}
			if (HasFixedVoltage)
			{
				return $"V = {_c}";
			}
			return $"V + {_b}I = {_c}";
		}
		public Complex CurrentAtVoltage(Complex voltage)
		{
			if (HasFixedVoltage)
				throw new Exception();
			if (HasFixedCurrent)
				return _c;
			return (_c - voltage) / _b;
		}

		public Complex VoltageAtCurrent(Complex current)
		{
			if (HasFixedCurrent)
				throw new Exception();
			if (HasFixedVoltage)
				return _c;
			return -_b * current + _c;
		}

		protected void OverwriteBy(LinearCurrentVoltageCharacteristic other)
		{
			_a = other._a;
			_b = other._b;
			_c = other._c;
		}
		#endregion

		#region Static Methods
		public static LinearCurrentVoltageCharacteristic operator ~(LinearCurrentVoltageCharacteristic characteristic)
		{
			characteristic._c = -characteristic._c;
			return characteristic;
		}
		public static LinearCurrentVoltageCharacteristic operator &(LinearCurrentVoltageCharacteristic first, LinearCurrentVoltageCharacteristic second)
		{
			if (first.HasFixedCurrent && second.HasFixedCurrent)
			{
				throw new Exception();
			}

			if (second.HasFixedCurrent)
			{
				return new LinearCurrentVoltageCharacteristic(first._a, first._b, first._c);
			}

			if (second.HasFixedCurrent)
			{
				return new LinearCurrentVoltageCharacteristic(second._a, second._b, second._c);
			}
			

			return new LinearCurrentVoltageCharacteristic(true, first._b + second._b, first._c + second._c);
		}
		public static LinearCurrentVoltageCharacteristic operator |(LinearCurrentVoltageCharacteristic first, LinearCurrentVoltageCharacteristic second)
		{
			if (first.HasFixedCurrent && second.HasFixedCurrent)
			{
				return new LinearCurrentVoltageCharacteristic(false, 1, first._c + second._c);
			}

			if (first.HasFixedCurrent && !second.HasFixedCurrent)
			{
				return new LinearCurrentVoltageCharacteristic(true, second._b, second._c + first._c * second._b);
			}

			if (!first.HasFixedCurrent && second.HasFixedCurrent)
			{
				return new LinearCurrentVoltageCharacteristic(true, first._b, first._c + second._c * first._b);
			}

			if (first.HasFixedVoltage && second.HasFixedVoltage)
			{
				throw new Exception();
			}

			return new LinearCurrentVoltageCharacteristic(
				true,
				(first._b * second._b) / (first._b + second._b),
				(first._c * second._b + second._c * first._b) / (first._b + second._b));
		}
		public static LinearCurrentVoltageCharacteristic OpenCircuit() => new LinearCurrentVoltageCharacteristic(false, Complex.One, Complex.Zero);
		public static LinearCurrentVoltageCharacteristic ShortCircuit() => new LinearCurrentVoltageCharacteristic(true, Complex.Zero, Complex.Zero);
		#endregion
	}
}
