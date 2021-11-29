using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day03;

internal class Program
{
	const string DEFAULT_INPUT_FILEPATH = "input.txt";

	private static int GetManhattanDistance(Point point1, Point point2)
	{
		return Math.Abs(point1.X - point2.X) + Math.Abs(point1.Y - point2.Y);
	}

	private static int GetIntersectionDistanceClosestToCenter(IEnumerable<Point> firstPath, IEnumerable<Point> secondPath)
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

	private static int GetIntersectionDistanceWithShortestDelay(IEnumerable<Point> firstPath, IEnumerable<Point> secondPath)
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

	private static void Main(string[] args)
	{
		string filepath = args.Length > 0 ? args[0] : DEFAULT_INPUT_FILEPATH;
		(string firstLine, string secondLine) = File.ReadAllLines(filepath).SplitIntoTwo();
		IEnumerable<Point> firstPath = firstLine.Split(',').Select(s => Vector2D.ParsePathStep(s)).ToPointPath(Point.Central);
		IEnumerable<Point> secondPath = secondLine.Split(',').Select(s => Vector2D.ParsePathStep(s)).ToPointPath(Point.Central);

		Console.Write("Part 1: ");
		int smallestDistanceToCenter = GetIntersectionDistanceClosestToCenter(firstPath, secondPath);
		Console.WriteLine(smallestDistanceToCenter);

		Console.Write("Part 2: ");
		int distanceWithShortestDelay = GetIntersectionDistanceWithShortestDelay(firstPath, secondPath);
		Console.WriteLine(distanceWithShortestDelay);
	}
}
