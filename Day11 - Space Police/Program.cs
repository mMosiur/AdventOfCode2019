using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Day11.Geometry;

namespace Day11;

internal class Program
{
	private const string DEFAULT_INPUT_FILEPATH = "input.txt";

	private static void PaintHull(Dictionary<Point, Color> hullPanels, HullPaintingRobot robot)
	{
		bool finished = false;
		while (!finished)
		{
			Color color = hullPanels.GetValueOrDefault(robot.Position, Color.Black);
			robot.Driver.Input.Enqueue((long)color);
			finished = robot.Driver.Run(breakOnOutputWritten: true);
			using (StreamReader reader = new(robot.Driver.OutputStream, leaveOpen: true))
			{
				if (reader.EndOfStream) continue;
				Color newColor = (Color)Convert.ToInt32(reader.ReadLine());
				hullPanels[robot.Position] = newColor;
			}
			finished = robot.Driver.Run(breakOnOutputWritten: true);
			using (StreamReader reader = new(robot.Driver.OutputStream, leaveOpen: true))
			{
				if (reader.EndOfStream) continue;
				int turn = Convert.ToInt32(reader.ReadLine());
				switch (turn)
				{
					case 0:
						robot.RotateCounterclockwise();
						break;
					case 1:
						robot.RotateClockwise();
						break;
					default:
						throw new InvalidOperationException();
				}
			}
			robot.GoForward();
		}
	}

	private static char ToChar(Color color) => color switch
	{
		Color.Black => '.',
		Color.White => '#',
		_ => throw new InvalidCastException()
	};

	private static void DisplayHull(Dictionary<Point, Color> hullPanels)
	{
		int xMin = int.MaxValue;
		int xMax = int.MinValue;
		int yMin = int.MaxValue;
		int yMax = int.MinValue;
		foreach (Point point in hullPanels.Keys)
		{
			if (point.X < xMin) xMin = point.X;
			else if (point.X > xMax) xMax = point.X;
			if (point.Y < yMin) yMin = point.Y;
			else if (point.Y > yMax) yMax = point.Y;
		}
		int xLength = xMax - xMin + 1;
		int yLength = yMax - yMin + 1;
		Color[,] hullColors = new Color[yLength, xLength];
		foreach ((Point point, Color color) in hullPanels)
		{
			hullColors[point.Y - yMin, point.X - xMin] = color;
		}
		for (int i = 0; i < hullColors.GetLength(0); i++)
		{
			for (int j = 0; j < hullColors.GetLength(1); j++)
			{
				Console.Write(ToChar(hullColors[i, j]));
			}
			Console.WriteLine();
		}
	}

	private static void Main(string[] args)
	{
		string filepath = args.Length > 0 ? args[0] : DEFAULT_INPUT_FILEPATH;
		long[] program = File.ReadAllText(filepath).Split(',').Select(long.Parse).ToArray();
		Stream outputStream = new ReadWriteStream();
		IEnumerable<long> inputValues = Enumerable.Empty<long>();
		IntcodeMachine machine = new(program, inputValues, outputStream);
		Dictionary<Point, Color> hull = new();
		HullPaintingRobot robot = new(machine);

		Console.Write("Part 1: ");
		PaintHull(hull, robot);
		int paintedPanelCount = hull.Count;
		Console.WriteLine(paintedPanelCount);

		Console.WriteLine("Part 2 message:");
		hull.Clear();
		machine.Reset();
		robot = new(machine);
		hull[robot.Position] = Color.White;
		PaintHull(hull, robot);
		DisplayHull(hull);
	}
}
