using System;

namespace Day03;

public struct ManhattanEdge
{
	public enum Orientation
	{
		Horizontal,
		Vertical
	}

	public Point Point1 { get; init; }
	public Point Point2 { get; init; }
	public Orientation Direction { get; init; }

	public Point LeftmostPoint => Point1.X <= Point2.X ? Point1 : Point2;
	public Point RightmostPoint => Point1.X <= Point2.X ? Point2 : Point1;
	public Point DownmostPoint => Point1.Y <= Point2.Y ? Point1 : Point2;
	public Point UpmostPoint => Point1.Y <= Point2.Y ? Point2 : Point1;

	public int Length => Direction switch
	{
		Orientation.Vertical => Math.Abs(Point1.Y - Point2.Y),
		Orientation.Horizontal => Math.Abs(Point1.X - Point2.X),
		_ => throw new InvalidOperationException()
	};

	public ManhattanEdge(Point point1, Point point2)
	{
		if (point1.X == point2.X)
		{
			Direction = Orientation.Vertical;
		}
		else if (point1.Y == point2.Y)
		{
			Direction = Orientation.Horizontal;
		}
		else throw new ArgumentException("Manhattan style edge needs to be vertical or horizontal");

		Point1 = point1;
		Point2 = point2;
	}

	public Point? GetCrossPoint(ManhattanEdge other)
	{
		if (Direction == other.Direction)
		{
			return null;
		}
		ManhattanEdge edgeVertical = Direction == Orientation.Vertical ? this : other;
		ManhattanEdge edgeHorizontal = Direction == Orientation.Horizontal ? this : other;
		int horizontalX = edgeVertical.Point1.X;
		if (horizontalX < edgeHorizontal.LeftmostPoint.X || horizontalX > edgeHorizontal.RightmostPoint.X)
		{
			return null;
		}
		int verticalY = edgeHorizontal.Point1.Y;
		if (verticalY < edgeVertical.DownmostPoint.Y || verticalY > edgeVertical.UpmostPoint.Y)
		{
			return null;
		}
		return new Point(horizontalX, verticalY);
	}
}
