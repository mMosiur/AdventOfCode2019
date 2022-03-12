using System;
using System.IO;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2019.Day01;

public class Day01Solver : DaySolver
{
	private readonly int[] _inputNumbers;

	public Day01Solver(string inputFilePath) : base(inputFilePath)
	{
		_inputNumbers = InputLines
			.Where(s => !string.IsNullOrWhiteSpace(s))
			.Select(s => int.Parse(s))
			.ToArray();
	}

	private int GetFuel(int mass)
	{
		return Math.Max((mass / 3) - 2, 0);
	}

	private int GetWholeFuel(int mass)
	{
		int result = 0;
		int additionalMass = GetFuel(mass);
		while (additionalMass > 0)
		{
			result += additionalMass;
			additionalMass = GetFuel(additionalMass);
		}
		return result;
	}

	public override string SolvePart1()
	{
		int result = _inputNumbers.Select(n => GetFuel(n)).Sum();
		return result.ToString();
	}

	public override string SolvePart2()
	{
		int result = _inputNumbers.Select(n => GetWholeFuel(n)).Sum();
		return result.ToString();
	}
}
