using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day13
{
	class Program
	{
		const string INPUT_FILEPATH = "input.txt";

		static void Main()
		{
			long[] program = File.ReadAllText(INPUT_FILEPATH).Split(',').Select(long.Parse).ToArray();
			Console.OutputEncoding = System.Text.Encoding.UTF8;
			IntcodeMachineScreen screen = new();

			Console.Write("Part 1: ");
			IntcodeMachine machine = new(program);
			screen.ConnectTo(machine);
			machine.Run();
			int count = screen.Enumerate().Select(kp => kp.Value).Count(c => c == Tile.Block.ToChar());
			Console.WriteLine(count);

			screen.Disconnect();

			Console.Write("Part 2: ");
			program[0] = 2;
			long getInput()
			{
				int ballX = screen.Enumerate().SingleOrDefault(kp => kp.Value == Tile.Ball.ToChar()).Key.X;
				int paddleX = screen.Enumerate().SingleOrDefault(kp => kp.Value == Tile.HorizontalPaddle.ToChar()).Key.X;
				return Comparer<int>.Default.Compare(ballX, paddleX);
			}
			machine = new IntcodeMachine(program, getInput);
			screen.ConnectTo(machine);
			machine.Run();
			Console.WriteLine(screen.Score);
		}
	}
}
