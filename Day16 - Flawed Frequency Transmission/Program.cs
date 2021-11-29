using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Day16;

const string DEFAULT_INPUT_FILEPATH = "input.txt";
const int OFFSET_NUMBER_LOCATION = 0;
const int OFFSET_NUMBER_LENGTH = 7;
const int RESULT_NUMBER_LENGTH = 8;
const int PHASE_COUNT = 100;
const int REPETITIONS_FOR_REAL_SIGNAL = 10_000;

string filepath = args.Length > 0 ? args[0] : DEFAULT_INPUT_FILEPATH;
IList<short> input = File.ReadAllText(filepath).Trim().Select(c => Convert.ToInt16(char.GetNumericValue(c))).ToList();
// Part 1
Console.Write("Part 1: ");
IList<short> result1 = FlawedFrequencyTransmission.CalculateFullPhases(input, PHASE_COUNT);
string part1 = string.Concat(result1.Take(RESULT_NUMBER_LENGTH));
Console.WriteLine(part1);
// Part 2
Console.Write("Part 2: ");
int offset = int.Parse(string.Concat(input.Skip(OFFSET_NUMBER_LOCATION).Take(OFFSET_NUMBER_LENGTH)));
IList<short> repeatedInput = input.RepeatSequence(REPETITIONS_FOR_REAL_SIGNAL).Skip(offset).ToList();
IList<short> result2 = FlawedFrequencyTransmission.CalculatePartiallyPhases(repeatedInput, PHASE_COUNT);
string part2 = string.Concat(result2.Take(RESULT_NUMBER_LENGTH));
Console.WriteLine(part2);

