using System;

namespace Day12
{
	public static class Numerical
	{
		public static ulong GCD(ulong a, ulong b)
		{
			while (a != 0 && b != 0)
			{
				if (a > b)
				{
					a %= b;
				}
				else
				{
					b %= a;
				}
			}
			return a | b;
		}

		public static ulong LCM(ulong a, ulong b) => a / GCD(a, b) * b;

		public static ulong LCM(long val1, long val2, params long[] values)
		{
			if (val1 == 0 || val2 == 0)
			{
				throw new ArgumentException("Argument cannot be zero.");
			}
			ulong lcm = LCM((ulong)System.Math.Abs(val1), (ulong)System.Math.Abs(val2));
			foreach (long value in values)
			{
				lcm = LCM(lcm, (ulong)System.Math.Abs(value));
			}
			return lcm;
		}
	}
}
