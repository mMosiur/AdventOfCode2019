using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Day14;

const string DEFAULT_INPUT_FILEPATH = "input.txt";

string filepath = args.Length > 0 ? args[0] : DEFAULT_INPUT_FILEPATH;
IList<Recipe> recipes = File.ReadAllLines(filepath).Select(Recipe.Parse).ToList();
FuelCalculator calculator = new(recipes, "FUEL", "ORE");
Console.Write("Part 1: ");
long part1 = calculator.GetRequiredOreForFuel(1);
Console.WriteLine(part1);
Console.Write("Part 2: ");
long part2 = calculator.GetMaxFuelForGivenOre(1_000_000_000_000);
Console.WriteLine(part2);
