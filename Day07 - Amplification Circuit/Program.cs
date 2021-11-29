using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day07;

internal class Program
{
	private const string DEFAULT_INPUT_FILEPATH = "input.txt";
	private const int START_POWER = 0;
	private const int NOF_AMPLIFIERS = 5;

	private static IEnumerable<IReadOnlyList<T>> GetPermutations<T>(IReadOnlyList<T> list)
	{
		if (list.Count <= 1)
		{
			yield return list;
			yield break;
		}
		for (int i = 0; i < list.Count; i++)
		{
			T el = list[i];
			foreach (IReadOnlyList<T> perm in GetPermutations(list.Take(i).Concat(list.Skip(i + 1)).ToList()))
			{
				yield return perm.Prepend(el).ToList();
			}
		}
	}

	private static void Main(string[] args)
	{
		string filepath = args.Length > 0 ? args[0] : DEFAULT_INPUT_FILEPATH;
		int[] input = File.ReadAllText(filepath).Split(',').Select(int.Parse).ToArray();
		ThrusterPowerSystem thrusterPowerSystem = new(input, NOF_AMPLIFIERS, START_POWER);

		thrusterPowerSystem.Reset();
		Console.Write("Part 1: ");
		int part1 = GetPermutations(Enumerable.Range(0, 5).ToList())
			.Max(thrusterPowerSystem.GetThrusterPower);
		Console.WriteLine(part1);

		thrusterPowerSystem.Reset();
		Console.Write("Part 2: ");
		int part2 = GetPermutations(Enumerable.Range(5, 5).ToList())
			.Max(perm => thrusterPowerSystem.GetRecurrentThrusterPower(perm, true));
		Console.WriteLine(part2);
	}
}
