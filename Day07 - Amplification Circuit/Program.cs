using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day07
{
	internal class Program
	{
		private const string INPUT_FILEPATH = "input.txt";
		private const int START_POWER = 0;
		private const int NOF_AMPLIFIERS = 5;

		public static IEnumerable<IReadOnlyList<T>> GetPermutations<T>(IReadOnlyList<T> list)
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

		private static void Main()
		{
			int[] input = File.ReadAllText(INPUT_FILEPATH).Split(',').Select(int.Parse).ToArray();
			ThrusterPowerSystem thrusterPowerSystem = new(input, NOF_AMPLIFIERS, START_POWER);

			thrusterPowerSystem.Reset();
			int max1 = GetPermutations(Enumerable.Range(0, 5).ToList())
				.Max(thrusterPowerSystem.GetThrusterPower);
			Console.WriteLine($"Part 1: {max1}");

			thrusterPowerSystem.Reset();
			int max2 = GetPermutations(Enumerable.Range(5, 5).ToList())
				.Max(perm => thrusterPowerSystem.GetRecurrentThrusterPower(perm, true));
			Console.WriteLine($"Part 2: {max2}");
		}
	}
}
