using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCircuitSolverCore.InternationalSystemOfUnits
{
	public enum Prefix
	{
		Yotta,
		Zetta,
		Exa,
		Peta,
		Tera,
		Giga,
		Mega,
		Kilo,
		None,
		Milli,
		Micro,
		Nano,
		Pico,
		Femto,
		Atto,
		Zepto,
		Yocto
	};
	public static class SIUnits
	{
		private static Dictionary<Prefix, double> _prefixValues = new Dictionary<Prefix, double>()
		{
			{Prefix.Yotta, 1e24},
			{Prefix.Zetta, 1e21},
			{Prefix.Exa, 1e18},
			{Prefix.Peta, 1e15},
			{Prefix.Tera, 1e12},
			{Prefix.Giga, 1e9},
			{Prefix.Mega, 1e6},
			{Prefix.Kilo, 1e3},
			{Prefix.None, 1},
			{Prefix.Milli, 1e-3},
			{Prefix.Micro, 1e-6},
			{Prefix.Nano, 1e-9},
			{Prefix.Pico, 1e-12},
			{Prefix.Femto, 1e-15},
			{Prefix.Atto, 1e-18},
			{Prefix.Zepto, 1e-21},
			{Prefix.Yocto, 1e-24}
		};
		public static double GetPrefixValue(Prefix prefix) => _prefixValues[prefix];
	}
}
