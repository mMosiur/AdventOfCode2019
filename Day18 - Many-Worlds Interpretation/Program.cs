using System;
using System.IO;
using System.Linq;

namespace Day18;

internal class Program
{
	private const string DEFAULT_INPUT_FILEPATH = "input.txt";

	private static void UpdateMapToFourSections(char[,] map)
	{
		bool updated = false;
		for (int i = 1; i < map.GetLength(0) - 1; i++)
		{
			for (int j = 1; j < map.GetLength(1) - 1; j++)
			{
				if (map[i - 1, j - 1] != '.') continue;
				if (map[i - 1, j] != '.') continue;
				if (map[i - 1, j + 1] != '.') continue;
				if (map[i, j - 1] != '.') continue;
				if (map[i, j] != '@') continue;
				if (map[i, j + 1] != '.') continue;
				if (map[i + 1, j - 1] != '.') continue;
				if (map[i + 1, j] != '.') continue;
				if (map[i + 1, j + 1] != '.') continue;
				if (updated)
				{
					throw new InvalidDataException(
						"Given map had more than one vault area matching requirements."
					);
				}
				map[i - 1, j - 1] = '@';
				map[i - 1, j] = '#';
				map[i - 1, j + 1] = '@';
				map[i, j - 1] = '#';
				map[i, j] = '#';
				map[i, j + 1] = '#';
				map[i + 1, j - 1] = '@';
				map[i + 1, j] = '#';
				map[i + 1, j + 1] = '@';
				updated = true;
			}
		}
		if (!updated)
		{
			throw new InvalidDataException(
				"Given map did not have one vault area matching requirements."
			);
		}
	}

	private static void Main(string[] args)
	{
		string filepath = args.Length > 0 ? args[0] : DEFAULT_INPUT_FILEPATH;
		string[] lines = File.ReadAllLines(filepath);
		char[,] mapArray = lines.Select(line => line.ToCharArray()).ToArray().ToRectangularArray();

		Console.Write("Part 1: ");
		TunnelMap map = TunnelMap.BuildFrom(mapArray, compress: true);
		TunnelMapRobots robots = new(map);
		int part1 = robots.ShortestRouteToAllKeys();
		Console.WriteLine(part1 == 3962);

		Console.Write("Part 2: ");
		UpdateMapToFourSections(mapArray);
		map = TunnelMap.BuildFrom(mapArray, compress: true);
		robots = new(map);
		int part2 = robots.ShortestRouteToAllKeys();
		Console.WriteLine(part2 == 1844);
	}
}
