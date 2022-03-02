using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2019.Day07;

public class Day07Solver : DaySolver
{
	private const int START_POWER = 0;
	private const int NOF_AMPLIFIERS = 5;

	private readonly int[] _input;
	private readonly ThrusterPowerSystem _thrusterPowerSystem;

	public Day07Solver(string inputFilePath) : base(inputFilePath)
	{
		_input = Input.Split(',').Select(int.Parse).ToArray();
		_thrusterPowerSystem = new(_input, NOF_AMPLIFIERS, START_POWER);
	}

	private IEnumerable<IReadOnlyList<T>> GetPermutations<T>(IReadOnlyList<T> list)
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

	public override string SolvePart1()
	{
		_thrusterPowerSystem.Reset();
		int part1 = GetPermutations(Enumerable.Range(0, 5).ToList())
			.Max(_thrusterPowerSystem.GetThrusterPower);
		return part1.ToString();
	}

	public override string SolvePart2()
	{
		_thrusterPowerSystem.Reset();
		int part2 = GetPermutations(Enumerable.Range(5, 5).ToList())
			.Max(perm => _thrusterPowerSystem.GetRecurrentThrusterPower(perm, true));
		return part2.ToString();
	}
}
