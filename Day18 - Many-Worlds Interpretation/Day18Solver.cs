using System.IO;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2019.Day18;

public class Day18Solver : DaySolver
{
	char[,] _mapArray;

	public Day18Solver(string inputFilePath) : base(inputFilePath)
	{
		_mapArray = InputLines
			.Select(line => line.ToCharArray())
			.ToArray()
			.ToRectangularArray();
	}

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

	public override string SolvePart1()
	{
		TunnelMap map = TunnelMap.BuildFrom(_mapArray, compress: true);
		TunnelMapRobots robots = new(map);
		int part1 = robots.ShortestRouteToAllKeys();
		return part1.ToString();
	}

	public override string SolvePart2()
	{
		char[,] mapArray = (char[,])_mapArray.Clone();
		UpdateMapToFourSections(mapArray);
		TunnelMap map = TunnelMap.BuildFrom(mapArray, compress: true);
		TunnelMapRobots robots = new(map);
		int part2 = robots.ShortestRouteToAllKeys();
		return part2.ToString();
	}
}
