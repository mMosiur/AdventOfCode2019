using System.IO;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2019.Day20;

public class Day20Solver : DaySolver
{
	private readonly char[,] _map;
	private readonly DonutMap _donutMap;

	public Day20Solver(string inputFilePath) : base(inputFilePath)
	{
		_map = ReadInputMap();
		_donutMap = DonutMap.BuildFromCharMap(_map);
	}

	private char[,] ReadInputMap()
	{
		string[] lines = InputLines.ToArray();
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

	public override string SolvePart1()
	{
		int part1 = _donutMap.ShortestPathPortals();
		return part1.ToString();
	}

	public override string SolvePart2()
	{
		int part2 = _donutMap.ShortestPathRecursiveSpaces();
		return part2.ToString();
	}
}
