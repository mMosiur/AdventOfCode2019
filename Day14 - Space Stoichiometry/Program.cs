using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day14
{
	class Program
	{
		const string INPUT_FILEPATH = "test3.txt";

		static void Main()
		{
			IList<Recipe> recipes = File.ReadAllLines(INPUT_FILEPATH).Select(Recipe.Parse).ToList();
			FuelCalculator calculator = new(recipes, "FUEL", "ORE");
			long part1 = calculator.GetRequiredOreForFuel(1);
			Console.WriteLine($"Part 1: {part1}");
			long part2 = calculator.GetMaxFuelForGivenOre(1000000000000);
			Console.WriteLine($"Part 2: {part2}");
		}
	}
}
