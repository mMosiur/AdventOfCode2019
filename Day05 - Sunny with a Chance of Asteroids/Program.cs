using System;
using System.IO;
using System.Linq;

using Day05;

const string DEFAULT_INPUT_FILEPATH = "input.txt";

string filepath = args.Length > 0 ? args[0] : DEFAULT_INPUT_FILEPATH;
int[] input = File.ReadAllText(filepath).Split(',').Select(int.Parse).ToArray();
int[] inputValues = new int[2]
{
	1, // Program ID for the first part
	5, // Program ID for the second part
};
using StringStream outputStream = new();

IntcodeMachine machine = new(input, inputValues, outputStream);

Console.Write("Part 1: ");
machine.Run();
string part1 = outputStream.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Last();
Console.WriteLine(part1);

machine.ResetMemory();
Console.Write("Part 2: ");
machine.Run();
string part2 = outputStream.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Last();
Console.WriteLine(part2);
