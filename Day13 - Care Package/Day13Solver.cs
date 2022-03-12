using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2019.Day13;

public class Day13Solver : DaySolver
{
	private long[] _program;
	private IntcodeMachineScreen _screen;

	public Day13Solver(string inputFilePath) : base(inputFilePath)
	{
		_program = Input.Split(',').Select(long.Parse).ToArray();
		_screen = new();
	}

	public override string SolvePart1()
	{
		_screen.Disconnect();
		IntcodeMachine machine = new(_program);
		_screen.ConnectTo(machine);
		machine.Run();
		int count = _screen.Enumerate()
			.Select(kp => kp.Value)
			.Count(c => c == Tile.Block.ToChar());
		_screen.Disconnect();
		return count.ToString();
	}

	public override string SolvePart2()
	{
		_screen.Disconnect();
		long[] program = _program;
		program[0] = 2;
		Func<long> getInput = () =>
		{
			int ballX = _screen.Enumerate().SingleOrDefault(kp => kp.Value == Tile.Ball.ToChar()).Key.X;
			int paddleX = _screen.Enumerate().SingleOrDefault(kp => kp.Value == Tile.HorizontalPaddle.ToChar()).Key.X;
			return (long)Comparer<int>.Default.Compare(ballX, paddleX);
		};
		IntcodeMachine machine = new(program, getInput);
		_screen.ConnectTo(machine);
		machine.Run();
		long score = _screen.Score;
		_screen.Disconnect();
		return score.ToString();
	}
}
