using System;

namespace Day12;

public static class Numerical
{
	public static ulong GCD(ulong a, ulong b)
	{
		while (a * b != 0)
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

	public static ulong LCM(ulong val1, ulong val2, params ulong[] values)
	{
		if (val1 == 0 || val2 == 0)
		{
			throw new ArgumentException("Argument cannot be zero.");
		}
		ulong lcm = LCM(val1, val2);
		foreach (ulong value in values)
		{
			lcm = LCM(lcm, value);
		}
		return lcm;
	}
}
