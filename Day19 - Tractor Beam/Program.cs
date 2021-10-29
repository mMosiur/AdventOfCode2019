using System;
using System.IO;
using System.Linq;

namespace Day19
{
	class Program
	{
		const string INPUT_FILEPATH = "input.txt";

		static void Main()
		{
			long[] program = File.ReadAllText(INPUT_FILEPATH).Split(',').Select(long.Parse).ToArray();
			DroneDispatcher dispatcher = new(program);

			const int AREA_WIDTH = 50;
			const int AREA_HEIGHT = 50;
			int part1 = dispatcher.CalculateTractorBeamArea(AREA_WIDTH, AREA_HEIGHT);
			Console.WriteLine($"Part 1: {part1}");

			const int SHIP_WIDTH = 100;
			const int SHIP_HEIGHT = 100;
			Point p = dispatcher.FindPointWhereShipFits(SHIP_WIDTH, SHIP_HEIGHT);
			int part2 = p.X * 10_000 + p.Y;
			Console.WriteLine($"Part 2: {part2}");
		}
	}
}
