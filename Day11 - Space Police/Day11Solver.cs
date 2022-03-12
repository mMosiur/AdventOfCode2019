using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AdventOfCode.Abstractions;
using AdventOfCode.Year2019.Day11.Geometry;

namespace AdventOfCode.Year2019.Day11;

public class Day11Solver : DaySolver
{
	private long[] program;

	public Day11Solver(string inputFilePath) : base(inputFilePath)
	{
		program = Input.Split(',').Select(long.Parse).ToArray();
	}

	private void PaintHull(Dictionary<Point, Color> hullPanels, HullPaintingRobot robot)
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

	private char ToChar(Color color) => color switch
	{
		Color.Black => '.',
		Color.White => '#',
		_ => throw new InvalidCastException()
	};

	private string DisplayHull(Dictionary<Point, Color> hullPanels)
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
		StringBuilder builder = new();
		for (int i = 0; i < hullColors.GetLength(0); i++)
		{
			for (int j = 0; j < hullColors.GetLength(1); j++)
			{
				builder.Append(ToChar(hullColors[i, j]));
			}
			builder.AppendLine();
		}
		return builder.ToString().TrimEnd();
	}

	public override string SolvePart1()
	{
		Stream outputStream = new ReadWriteStream();
		IEnumerable<long> inputValues = Enumerable.Empty<long>();
		IntcodeMachine machine = new(program, inputValues, outputStream);
		Dictionary<Point, Color> hull = new();
		HullPaintingRobot robot = new(machine);

		PaintHull(hull, robot);
		int paintedPanelCount = hull.Count;
		return paintedPanelCount.ToString();
	}

	public override string SolvePart2()
	{
		Stream outputStream = new ReadWriteStream();
		IEnumerable<long> inputValues = Enumerable.Empty<long>();
		IntcodeMachine machine = new(program, inputValues, outputStream);
		Dictionary<Point, Color> hull = new();
		HullPaintingRobot robot = new(machine);
		robot = new(machine);
		hull[robot.Position] = Color.White;
		PaintHull(hull, robot);
		return DisplayHull(hull);
	}
}
