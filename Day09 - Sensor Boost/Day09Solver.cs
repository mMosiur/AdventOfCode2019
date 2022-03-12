using System;
using System.IO;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2019.Day09;

public class Day09Solver : DaySolver
{
	private long[] _program;

	public Day09Solver(string inputFilePath) : base(inputFilePath)
	{
		_program = Input.Split(',').Select(long.Parse).ToArray();
	}

	public override string SolvePart1()
	{
		IntcodeMachine machine = new(_program);
		using StringStream outputStream = new();
		machine.OutputStream = outputStream;
		machine.InputValues = new long[] { 1 };
		machine.Run();
		string result = outputStream.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Single();
		return result;
	}

	public override string SolvePart2()
	{
		IntcodeMachine machine = new(_program);
		using StringStream outputStream = new();
		machine.OutputStream = outputStream;
		machine.InputValues = new long[] { 2 };
		machine.Run();
		string result = outputStream.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Single();
		return result;
	}
}
