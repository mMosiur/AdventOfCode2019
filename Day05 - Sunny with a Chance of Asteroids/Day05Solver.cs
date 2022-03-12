using System;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2019.Day05;

public class Day05Solver : DaySolver
{
	private readonly int[] _input;

	public Day05Solver(string inputFilePath) : base(inputFilePath)
	{
		_input = Input.Trim().Split(',').Select(int.Parse).ToArray();
	}

	public override string SolvePart1()
	{
		int[] inputValues = new int[1]
		{
			1, // Program ID for the first part
		};
		using StringStream outputStream = new();
		IntcodeMachine machine = new(_input, inputValues, outputStream);
		machine.Run();
		string output = outputStream
			.ToString()
			.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
			.Last();
		return output;
	}

	public override string SolvePart2()
	{
		int[] inputValues = new int[1]
		{
			5, // Program ID for the second part
		};
		using StringStream outputStream = new();
		IntcodeMachine machine = new(_input, inputValues, outputStream);
		machine.Run();
		string output = outputStream
			.ToString()
			.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
			.Last();
		return output;
	}
}
