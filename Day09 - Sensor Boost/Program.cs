using System;
using System.IO;
using System.Linq;

namespace Day09
{
	class Program
	{
		const string INPUT_FILEPATH = "input.txt";

		static void Main()
		{
			long[] program = File.ReadAllText(INPUT_FILEPATH).Split(',').Select(long.Parse).ToArray();
			IntcodeMachine machine = new(program);
			machine.OutputStream = Console.OpenStandardOutput();

			// Part 1
			Console.Write("Part 1: ");
			machine.InputValues = new long[] { 1 };
			machine.Run();

			machine.ResetMachine();

			// Part 2
			Console.Write("Part 2: ");
			machine.InputValues = new long[] { 2 };
			machine.Run();
		}
	}
}
