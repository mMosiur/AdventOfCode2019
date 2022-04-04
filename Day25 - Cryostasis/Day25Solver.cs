using System.Text;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2019.Day25;

public class Day25Solver : DaySolver
{
	private readonly long[] _program;
	private Queue<long> _inputQueue = new();

	private void EnqueueCommandFrom(string filename)
	{
		IEnumerable<string> commands = File.ReadLines(filename)
			.Where(line => !string.IsNullOrWhiteSpace(line)) // Ignore empty lines
			.Select(line => line.Trim()) // Trim lines
			.Where(line => !line.StartsWith("#")); // Ignore comments
		EnqueueCommands(commands);
	}

	private void EnqueueCommands(IEnumerable<string> commands)
	{
		foreach (var command in commands)
		{
			foreach (char c in command)
			{
				_inputQueue.Enqueue((long)c);
			}
			_inputQueue.Enqueue((long)'\n');
		}
	}

	private long GetFromInputQueue()
	{
		if (_inputQueue.Count == 0)
		{
			string line = Console.ReadLine() ?? "";
			while (string.IsNullOrWhiteSpace(line))
			{
				line = Console.ReadLine() ?? "";
			}
			foreach (char c in line)
			{
				_inputQueue.Enqueue((long)c);
			}
			_inputQueue.Enqueue((long)'\n');
		}
		return _inputQueue.Dequeue();
	}

	public Day25Solver(string inputFilePath) : base(inputFilePath)
	{
		_program = Input.Split(',').Select(long.Parse).ToArray();
	}

	public override string SolvePart1()
	{
		var machine = new IntcodeMachine(
			_program,
			GetFromInputQueue
		);
		EnqueueCommandFrom("commands-collect-all-items.txt");
		foreach (long output in machine.RunYieldingOutput())
		{
			Console.Write((char)output);
			Console.Out.Flush();
		}
		return "UNSOLVED";
	}

	public override string SolvePart2()
	{
		return "UNSOLVED";
	}
}
