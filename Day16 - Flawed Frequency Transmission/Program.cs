using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day16
{
	class Program
	{
		private const string INPUT_FILEPATH = "input.txt";
		private const int OFFSET_NUMBER_LOCATION = 0;
		private const int OFFSET_NUMBER_LENGTH = 7;
		private const int RESULT_NUMBER_LENGTH = 8;
		private const int PHASE_COUNT = 100;
		private const int REPETITIONS_FOR_REAL_SIGNAL = 10_000;

		static string Part1(IList<short> input)
		{
			IList<short> result1 = FlawedFrequencyTransmission.CalculateFullPhases(input, PHASE_COUNT);
			return string.Concat(result1.Take(RESULT_NUMBER_LENGTH));
		}

		static string Part2(IList<short> input)
		{
			int offset = int.Parse(string.Concat(input.Skip(OFFSET_NUMBER_LOCATION).Take(OFFSET_NUMBER_LENGTH)));
			IList<short> repeatedInput = input.RepeatSequence(REPETITIONS_FOR_REAL_SIGNAL).Skip(offset).ToList();
			IList<short> result2 = FlawedFrequencyTransmission.CalculatePartiallyPhases(repeatedInput, PHASE_COUNT);
			return string.Concat(result2.Take(RESULT_NUMBER_LENGTH));
		}

		static void Main()
		{
			IList<short> input = File.ReadAllText(INPUT_FILEPATH).Trim().Select(c => Convert.ToInt16(char.GetNumericValue(c))).ToList();
			string part1 = Part1(input);
			Console.WriteLine($"Part 1: {part1}");
			string part2 = Part2(input);
			Console.WriteLine($"Part 2: {part2}");
		}
	}
}
