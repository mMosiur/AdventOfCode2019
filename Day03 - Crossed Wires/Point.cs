namespace Day03;

public struct Point
{
	public int X { get; init; }
	public int Y { get; init; }

	public Point(int x, int y)
	{
		X = x;
		Y = y;
	}

	public static Point Central => new(0, 0);

	public static Point operator +(Point point, Vector2D vector) => new(point.X + vector.X, point.Y + vector.Y);
}
