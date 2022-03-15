using System.Numerics;

namespace AdventOfCode.Year2019.Day22;

public class ModuloCalculator
{
	public BigInteger Modulus { get; }

	public bool AllowNegative { get; }

	public ModuloCalculator(BigInteger modulus, bool allowNegative = true)
	{
		this.Modulus = modulus;
		AllowNegative = allowNegative;
	}

	public BigInteger ModAdd(BigInteger a, BigInteger b)
	{
		BigInteger result = (a + b) % Modulus;
		if (result < 0 && !AllowNegative)
		{
			result += Modulus;
		}
		return result;
	}

	public BigInteger ModMultiply(BigInteger a, BigInteger b)
	{
		BigInteger result = (a * b) % Modulus;
		if (result < 0 && !AllowNegative)
		{
			result += Modulus;
		}
		return result;
	}

	public BigInteger ModMultiply(BigInteger a, BigInteger b, BigInteger c)
	{
		BigInteger result = (a * b * c) % Modulus;
		if (result < 0 && !AllowNegative)
		{
			result += Modulus;
		}
		return result;
	}

	public BigInteger ModPower(BigInteger value, BigInteger exponent)
	{
		BigInteger result = BigInteger.ModPow(value, exponent, Modulus);
		if (result < 0 && !AllowNegative)
		{
			result += Modulus;
		}
		return result;
	}

	public BigInteger Mod(BigInteger number)
	{
		BigInteger result = number % Modulus;
		if (result < 0 && !AllowNegative)
		{
			result += Modulus;
		}
		return result;
	}

	public BigInteger ModInverse(BigInteger value)
	{
		BigInteger result = BigInteger.ModPow(value, Modulus - 2, Modulus);
		if (result < 0 && !AllowNegative)
		{
			result += Modulus;
		}
		return result;
	}
}
