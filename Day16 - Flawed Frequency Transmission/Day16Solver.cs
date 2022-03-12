using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2019.Day16;

public class Day16Solver : DaySolver
{
	const int OFFSET_NUMBER_LOCATION = 0;
	const int OFFSET_NUMBER_LENGTH = 7;
	const int RESULT_NUMBER_LENGTH = 8;
	const int PHASE_COUNT = 100;
	const int REPETITIONS_FOR_REAL_SIGNAL = 10_000;

	IList<short> _input;

	public Day16Solver(string inputFilePath) : base(inputFilePath)
	{
		_input = Input
			.Trim()
			.Select(c => Convert.ToInt16(char.GetNumericValue(c)))
			.ToList();
	}

	public override string SolvePart1()
	{
		IList<short> result1 = FlawedFrequencyTransmission.CalculateFullPhases(_input, PHASE_COUNT);
		string part1 = string.Concat(result1.Take(RESULT_NUMBER_LENGTH));
		return part1;
	}

	public override string SolvePart2()
	{
		int offset = int.Parse(string.Concat(_input.Skip(OFFSET_NUMBER_LOCATION).Take(OFFSET_NUMBER_LENGTH)));
		IList<short> repeatedInput = _input.RepeatSequence(REPETITIONS_FOR_REAL_SIGNAL).Skip(offset).ToList();
		IList<short> result2 = FlawedFrequencyTransmission.CalculatePartiallyPhases(repeatedInput, PHASE_COUNT);
		string part2 = string.Concat(result2.Take(RESULT_NUMBER_LENGTH));
		return part2;
	}
}
