using System;
using System.IO;
using System.Linq;

namespace Day05
{
	class Program
	{
		static void Main()
		{
			int[] input = File.ReadAllText("input.txt").Split(',').Select(s => int.Parse(s)).ToArray();
			int[] inputValues = new int[2]
			{
				1, // Program ID for the first part
				5, // Program ID for the second part
			};
			Stream outputStream = Console.OpenStandardOutput();
			IntcodeMachine machine = new(input, inputValues, outputStream);
			Console.WriteLine("Running test for part 1:");
			machine.Run();
			machine.ResetMemory();
			Console.WriteLine("Running tests for part 2:");
			machine.Run();
		}
	}
}
