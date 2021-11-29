using System;
using System.IO;
using System.Linq;

namespace Day20;

internal class Program
{
	private const string DEFAULT_INPUT_FILEPATH = "input.txt";

	private static char[,] ReadInput(string filepath)
	{
		string[] lines = File.ReadAllLines(filepath);
		if (lines.Length == 0)
		{
			throw new FileLoadException("Input file was empty.");
		}
		int width = lines[0].Length;
		if (width == 0 || !lines.All(line => line.Length == width))
		{
			throw new FileLoadException("Input file was not in a correct format.");
		}
		char[,] map = new char[lines.Length, width];
		for (int i = 0; i < lines.Length; i++)
		{
			for (int j = 0; j < width; j++)
			{
				map[i, j] = lines[i][j];
			}
		}
		return map;
	}

	private static void Main(string[] args)
	{
		string filepath = args.Length > 0 ? args[0] : DEFAULT_INPUT_FILEPATH;
		char[,] input = ReadInput(filepath);
		DonutMap donutMap = DonutMap.BuildFromCharMap(input);
		Console.Write("Part 1: ");
		int part1 = donutMap.ShortestPathPortals();
		Console.WriteLine(part1);
		Console.Write("Part 2: ");
		int part2 = donutMap.ShortestPathRecursiveSpaces();
		Console.WriteLine(part2);
	}
}
