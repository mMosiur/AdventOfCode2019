using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2019.Day14;

public class Day14Solver : DaySolver
{
	FuelCalculator _calculator;

	public Day14Solver(string inputFilePath) : base(inputFilePath)
	{
		IList<Recipe> recipes = InputLines.Select(Recipe.Parse).ToList();
		_calculator = new(recipes, "FUEL", "ORE");
	}

	public override string SolvePart1()
	{
		long part1 = _calculator.GetRequiredOreForFuel(1);
		return part1.ToString();
	}

	public override string SolvePart2()
	{
		long part2 = _calculator.GetMaxFuelForGivenOre(1_000_000_000_000);
		return part2.ToString();
	}
}
