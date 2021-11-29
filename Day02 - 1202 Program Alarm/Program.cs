using System;
using System.IO;
using System.Linq;

namespace Day02;

internal class Program
{
	const string DEFAULT_INPUT_FILEPATH = "input.txt";
	const int INITIAL_NOUN = 12;
	const int INITIAL_VERB = 2;
	const int NOUN_VERB_TARGET = 19690720;

	private static int GetNounVerbValue(int[] input, int target)
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

	private static void Main(string[] args)
	{
		string filepath = args.Length > 0 ? args[0] : DEFAULT_INPUT_FILEPATH;
		int[] input = File.ReadAllText(filepath).Split(',').Select(s => int.Parse(s)).ToArray();

		Console.Write("Part 1: ");
		IntcodeMachine machine = new(input, INITIAL_NOUN, INITIAL_VERB);
		machine.Run();
		Console.WriteLine(machine.Memory[0]);

		Console.Write("Part 2: ");
		int result = GetNounVerbValue(input, NOUN_VERB_TARGET);
		System.Console.WriteLine(result);
	}
}
