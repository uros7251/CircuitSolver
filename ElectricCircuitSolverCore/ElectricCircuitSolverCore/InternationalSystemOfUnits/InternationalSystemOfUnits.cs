using System.Numerics;

namespace ElectricCircuitSolverCore.InternationalSystemOfUnits
{
	public enum Prefix
	{
		Yotta = 89,
		Zetta = 90,
		Exa = 69,
		Peta = 80,
		Tera = 84,
		Giga = 71,
		Mega = 77,
		Kilo = 75,
		None,
		Milli = 109,
		Micro = 117,
		Nano = 110,
		Pico = 112,
		Femto = 102,
		Atto = 97,
		Zepto = 122,
		Yocto = 121
	};
	public class Prefixes
	{
		private static Dictionary<Prefix, double> _prefixValues = new Dictionary<Prefix, double>(){
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
		public static double GetPrefixValue(char prefixLetter)
		{
			if (prefixLetter == 'k') prefixLetter = 'K';
			else if (prefixLetter == 'μ') prefixLetter = 'u';
			if (Enum.IsDefined(typeof(Prefix), (int)prefixLetter))
			{
				Prefix prefix = (Prefix)prefixLetter;
				return _prefixValues[prefix];
			}
			throw new Exception($"'{prefixLetter}' is not valid prefix.");
		}
		public static double GetPrefixValue(Prefix prefix)
		{
			return _prefixValues[prefix];
		}
	}
}
