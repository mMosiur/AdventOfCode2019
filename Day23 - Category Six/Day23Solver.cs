using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2019.Day23;

public class Day23Solver : DaySolver
{
	private readonly long[] _program;

	private IntcodeNetwork _network;
	private Task? _networkTask;

	public Day23Solver(string inputFilePath) : base(inputFilePath)
	{
		_program = Input.Split(',').Select(long.Parse).ToArray();
		_network = new IntcodeNetwork(_program, 50, verbose: false);
	}

	public override string SolvePart1()
	{
		if (_networkTask is null)
		{
			_networkTask = _network.Run();
		}
		long result = _network.GetFirstNatReceivedY();
		return result.ToString();
	}

	public override string SolvePart2()
	{
		if (_networkTask is null)
		{
			_networkTask = _network.Run();
		}
		long result = _network.GetLastNatSentY();
		return result.ToString();
	}
}
