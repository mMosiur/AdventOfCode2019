using AdventOfCode.Abstractions;
using Combinatorics.Collections;

namespace AdventOfCode.Year2019.Day25;

public class Day25Solver : DaySolver
{
	private readonly long[] _program;
	private readonly List<string> _items;

	public Day25Solver(string inputFilePath) : base(inputFilePath)
	{
		_program = Input.Split(',').Select(long.Parse).ToArray();
		_items = File.ReadLines("items.txt")
			.Where(line => !string.IsNullOrWhiteSpace(line))
			.Select(line => line.Trim())
			.ToList();
	}

	public override string SolvePart1()
	{
		var queue = new IntcodeMachineCommandQueue();
		var machine = new IntcodeMachine(
			_program,
			queue.GetInput
		);
		queue.EnqueueCommandsFrom("commands-collect-all-items.txt");
		// Start with all items dropped
		for (int i = 1; i <= _items.Count; i++)
		{
			// For every possible size of items subset get all combinations of that size
			var combinations = new Combinations<string>(_items, i);
			foreach (var combination in combinations)
			{
				// For each combination enqueue command to take all of them
				queue.EnqueueCommands(combination.Select(item => $"take {item}"));
				// Try going into the pressure plate room
				queue.EnqueueCommand("east");
				// If we are correct, the next command will not be executed. Otherwise:
				// Drop all items in the combination
				queue.EnqueueCommands(combination.Select(item => $"drop {item}"));
			}
		}
		string? lastDigitSequence = null;
		bool lastDigitSequenceBroken = false;
		foreach (long output in machine.RunYieldingOutput())
		{
			char character = (char)output;
			if(char.IsDigit(character))
			{
				if(lastDigitSequenceBroken)
				{
					lastDigitSequence = null;
				}
				lastDigitSequence += character;
				lastDigitSequenceBroken = false;
			}
			else
			{
				lastDigitSequenceBroken = true;
			}
		}
		return lastDigitSequence ?? throw new Exception("No digit sequence found");;
	}

	public override string SolvePart2() => string.Empty;
}
