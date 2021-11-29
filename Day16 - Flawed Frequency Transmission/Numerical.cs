using System;

namespace Day16;

public static class Numerical
{
	public static short GetLastDigit(long number) => (short)(Math.Abs(number) % 10);
}
