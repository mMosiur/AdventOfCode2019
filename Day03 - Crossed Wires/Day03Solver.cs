using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2019.Day03;

public class Day03Solver : DaySolver
{
	private string _firstLine;
	private string _secondLine;

	private Lazy<IEnumerable<Point>> FirstPath { get; }
	private Lazy<IEnumerable<Point>> SecondPath { get; }

	public Day03Solver(string inputFilePath) : base(inputFilePath)
	{
		var it = InputLines.GetEnumerator();
		if (!it.MoveNext() || string.IsNullOrWhiteSpace(it.Current)) throw new InvalidOperationException("Input file is empty");
		_firstLine = it.Current;
		if (!it.MoveNext() || string.IsNullOrWhiteSpace(it.Current)) throw new InvalidOperationException("Input file consists of only one line");
		_secondLine = it.Current;
		while (it.MoveNext())
		{
			if (!string.IsNullOrWhiteSpace(it.Current))
			{
				throw new InvalidOperationException("Input file contains more than two lines");
			}
		}
		FirstPath = new Lazy<IEnumerable<Point>>(
			() => _firstLine.Split(',').Select(s => Vector2D.ParsePathStep(s)).ToPointPath(Point.Central)
		);
		SecondPath = new Lazy<IEnumerable<Point>>(
			() => _secondLine.Split(',').Select(s => Vector2D.ParsePathStep(s)).ToPointPath(Point.Central)
		);
	}

	private int GetManhattanDistance(Point point1, Point point2)
	{
		return Math.Abs(point1.X - point2.X) + Math.Abs(point1.Y - point2.Y);
	}

	private int GetIntersectionDistanceClosestToCenter(IEnumerable<Point> firstPath, IEnumerable<Point> secondPath)
	{
		int smallestDistance = int.MaxValue;
		foreach (ManhattanEdge edge1 in firstPath.EdgesInPath())
		{
			foreach (ManhattanEdge edge2 in secondPath.EdgesInPath())
			{
				Point? crossPoint = edge1.GetCrossPoint(edge2);
				if (crossPoint is null) continue;
				int distance = GetManhattanDistance((Point)crossPoint, Point.Central);
				if (distance < smallestDistance)
				{
					smallestDistance = distance;
				}
			}
		}
		return smallestDistance;
	}

	private int GetIntersectionDistanceWithShortestDelay(IEnumerable<Point> firstPath, IEnumerable<Point> secondPath)
	{
		int smallestDistance = int.MaxValue;
		int firstTotalDistanceTravelled = 0;
		foreach (ManhattanEdge edge1 in firstPath.EdgesInPath())
		{
			int secondTotalDistanceTravelled = 0;
			foreach (ManhattanEdge edge2 in secondPath.EdgesInPath())
			{
				Point? crossPoint = edge1.GetCrossPoint(edge2);
				if (crossPoint is not null)
				{
					int distance =
						firstTotalDistanceTravelled +
						secondTotalDistanceTravelled +
						new ManhattanEdge(edge1.Point1, (Point)crossPoint).Length +
						new ManhattanEdge(edge2.Point1, (Point)crossPoint).Length;

					if (distance < smallestDistance)
					{
						smallestDistance = distance;
					}
				}
				secondTotalDistanceTravelled += edge2.Length;
			}
			firstTotalDistanceTravelled += edge1.Length;
		}
		return smallestDistance;
	}

	public override string SolvePart1()
	{
		int smallestDistanceToCenter = GetIntersectionDistanceClosestToCenter(FirstPath.Value, SecondPath.Value);
		return smallestDistanceToCenter.ToString();
	}

	public override string SolvePart2()
	{
		int distanceWithShortestDelay = GetIntersectionDistanceWithShortestDelay(FirstPath.Value, SecondPath.Value);
		return distanceWithShortestDelay.ToString();
	}
}
