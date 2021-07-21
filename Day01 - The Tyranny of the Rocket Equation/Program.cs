using System;
using System.IO;
using System.Linq;

namespace Day01
{
	class Program
	{
		const string INPUT_FILE_PATH = "input.txt";

		static int GetFuel(int mass)
		{
			return Math.Max((mass / 3) - 2, 0);
		}

		static int GetWholeFuel(int mass)
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

		static void Main()
		{
			int x = GetWholeFuel(1969);
			int[] inputNumbers = File.ReadAllLines(INPUT_FILE_PATH)
				.Where(s => !string.IsNullOrWhiteSpace(s))
				.Select(s => int.Parse(s))
				.ToArray();

			Console.Write("Part 1: ");
			int part1 = inputNumbers.Select(n => GetFuel(n)).Sum();
			Console.WriteLine(part1);

			Console.Write("Part 2: ");
			int part2 = inputNumbers.Select(n => GetWholeFuel(n)).Sum();
			Console.WriteLine(part2);
		}
	}
}
