using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Day13;

const string DEFAULT_INPUT_FILEPATH = "input.txt";

string filepath = args.Length > 0 ? args[0] : DEFAULT_INPUT_FILEPATH;
long[] program = File.ReadAllText(filepath).Split(',').Select(long.Parse).ToArray();
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
