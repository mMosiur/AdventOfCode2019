using System;
using System.IO;
using System.Linq;

namespace Day04;

internal class Program
{
	private const string DEFAULT_INPUT_FILEPATH = "input.txt";

	private static void Main(string[] args)
	{
		string filepath = args.Length > 0 ? args[0] : DEFAULT_INPUT_FILEPATH;
		(int start, int end) = File.ReadAllText(filepath)
			.Split('-').Select(s => int.Parse(s)).SplitIntoTwo();
		PasswordChecker passwordChecker = new(start, end);

		Console.Write("Part 1: ");
		int part1 = passwordChecker.AllMatchingPassword(false).Count();
		Console.WriteLine(part1);

		Console.Write("Part 2: ");
		int part2 = passwordChecker.AllMatchingPassword(true).Count();
		Console.WriteLine(part2);
	}
}
