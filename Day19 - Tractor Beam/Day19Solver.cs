using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2019.Day19;

public class Day19Solver : DaySolver
{
	const string DEFAULT_INPUT_FILEPATH = "input.txt";
	const int AREA_WIDTH = 50;
	const int AREA_HEIGHT = 50;
	const int SHIP_WIDTH = 100;
	const int SHIP_HEIGHT = 100;

	private readonly long[] _program;
	DroneDispatcher _dispatcher;

	public Day19Solver(string inputFilePath) : base(inputFilePath)
	{
		_program = Input.Split(',').Select(long.Parse).ToArray();
		_dispatcher = new DroneDispatcher(_program);
	}

	public override string SolvePart1()
	{
		int part1 = _dispatcher.CalculateTractorBeamArea(AREA_WIDTH, AREA_HEIGHT);
		return part1.ToString();
	}

	public override string SolvePart2()
	{
		Point p = _dispatcher.FindPointWhereShipFits(SHIP_WIDTH, SHIP_HEIGHT);
		int part2 = p.X * 10_000 + p.Y;
		return part2.ToString();
	}
}
