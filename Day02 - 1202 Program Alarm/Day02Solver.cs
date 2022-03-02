using System;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2019.Day02;

public class Day02Solver : DaySolver
{
	const int INITIAL_NOUN = 12;
	const int INITIAL_VERB = 2;
	const int NOUN_VERB_TARGET = 19690720;

	private readonly int[] _input;

	public Day02Solver(string inputFilePath) : base(inputFilePath)
	{
		_input = Input.Split(',').Select(int.Parse).ToArray();
	}

	private int GetNounVerbValue(int[] input, int target)
	{
		IntcodeMachine machine = new(input);
		for (int noun = 0; noun <= 99; noun++)
		{
			for (int verb = 0; verb <= 99; verb++)
			{
				machine.ResetMemory(noun, verb);
				machine.Run();
				if (machine.Memory[0] == target)
				{
					return 100 * noun + verb;
				}
			}
		}
		throw new Exception();
	}

	public override string SolvePart1()
	{
		IntcodeMachine machine = new(_input, INITIAL_NOUN, INITIAL_VERB);
		machine.Run();
		int result = machine.Memory[0];
		return result.ToString();
	}

	public override string SolvePart2()
	{
		int result = GetNounVerbValue(_input, NOUN_VERB_TARGET);
		return result.ToString();
	}
}
