namespace AdventOfCode.Year2019.Day11.Geometry;

public record struct Point(int X, int Y)
{
	public static Point operator +(Point point, Vector vector)
	{
		return new Point()
		{
			X = point.X + vector.X,
			Y = point.Y + vector.Y
		};
	}
}
